require "test/unit"
require "./string_calculator.rb"

class StringCalculatorTests < Test::Unit::TestCase

  def setup
    @calculator = StringCalculator.new
  end

  def test_nil_string_returns_0
    result = @calculator.add nil
    self.assert_equal(0, result)
  end

  def test_empty_string_returns_0
    result = @calculator.add ''
    self.assert_equal(0, result)
  end

  def test_one_value_returns_this_value
    result = @calculator.add '5'
    self.assert_equal(5, result)
  end

  def test_two_values_returns_their_sum
    result = @calculator.add '5,7'
    self.assert_equal(12, result)
  end

  def test_unknown_amount_of_vals_returns_sum
    values = (1..rand(100)).to_a.each{rand(100)}.to_a
    puts values
  end
end