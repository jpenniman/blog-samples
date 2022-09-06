Feature: Users can search for Customers in the system.

Scenario: Search by a company name beginning with the search term.
	Given the following customers
		| CompanyName |
		| Hungry Joes |
	When the company name begins with the term "Hungry"
	Then then 1 record should be returned.