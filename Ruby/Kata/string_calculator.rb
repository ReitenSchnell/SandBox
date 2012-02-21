class StringCalculator
  def add(number)
    if number.to_s == ''
      return 0
    else
      values = []
      number.split(',').each {|s| values.push(s.to_i)}
      return values.inject(:+)
    end
  end
end