require 'test_helper'

class ProductTest < ActiveSupport::TestCase
  setup do
	@product = products(:one)
	@update = {
	:title => 'Lorem Ipsum',
	:description => 'Wibbles are fun',
	:image_url => 'lorem.jpg',
	:price = 19.95	
	}
  end	
	
  test "should get index" do
	get :index
	assert_response :success
	assert_not_nil assigns(:products)
  end  
  
  test "should get index" do
	get :new
	assert_response :success	
  end  
  
  test "should create product" do
	assert_difference('Product.count') do
		post :create, :product => @update
	end	
	assert_redirected_to_product_path(assigns(:product))
  end  
end
