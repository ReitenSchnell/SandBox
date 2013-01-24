Feature: Looking for perfect numbers	

Scenario: Nonperfect number
	Given Nonperfect number	
	When Asked if the number is perfect
	Then the result should be False

Scenario: Perfect number
	Given Perfect number	
	When Asked if the number is not perfect
	Then the result should be True