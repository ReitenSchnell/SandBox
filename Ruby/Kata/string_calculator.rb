class StringCalculator
  def add(number)
    if number.to_s ==  ''
       return 0
    end
    delimiters = /[,\\n]/
    if number[0..2] == '//['
      delimiters = number[3]
      number = number[7..-1]
    end
    values = number.split(delimiters).map(&:to_i)
    negatives = values.find_all{|item| item < 0}
    if not negatives.empty?
      raise ArgumentError, 'Negatives are not allowed: '+negatives.join(',')
    end
    return values.inject(:+)
  end
end