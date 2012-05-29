require 'test_helper'

class ProductTest < ActiveSupport::TestCase
	test 'product attributes must not be empty' do
		product = Product.new
		assert product.invalid?
		assert product.errors[:title].any?
		assert product.errors[:description].any?
		assert product.errors[:image_url].any?
		assert product.errors[:price].any?
	end
	
	test 'product price must be positive' do
		product = Product.new(
			:title => "Book",
			:description => "Description",
			:image_url => "zzz.jpg"
		)
		
		product.price = -1
		assert product.invalid?
		assert_equal 'must be greater than or equal to 0.01', product.errors[:price].join('; ')
		
		product.price = 0
		assert product.invalid?
		assert_equal 'must be greater than or equal to 0.01', product.errors[:price].join('; ')
		
		product.price = 1
		assert product.valid?		
	end
	
	def new_product(image_url) 
		product = Product.new(
			:title => "Book",
			:description => "Description",
			:image_url => image_url,
			:price => 1
		)
	end
	
	test 'image url' do
		ok = %w{fred.jpg fred.gif fred.png FRED.jpg FRED.gif http://f.c/x/y/z/fred.gif}
		bad = %w{fred.doc fred.gif/more fred.png.more}
		ok.each do |name|
			assert new_product(name).valid?, "#{name} should't be invalid"
		end
		bad.each do |name|
			assert new_product(name).invalid?, "#{name} shoudn't be valid" 
		end
  end

  test 'product is not valid without a unique title' do
    product = Product.new(
          :title => products(:ruby).title,
    			:description => "Description",
    			:image_url => 'fred.gif',
    			:price => 1)
    assert !product.save
    assert_equal 'has already been taken', product.errors[:title].join('; ')
  end
end
