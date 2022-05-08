using FluentAssertions;
using TechTalk.SpecFlow.Assist;

namespace CustomerManagement.Specifications.Steps;

[Binding]
public class SearchScenario
{
    readonly ScenarioContext _scenarioContext;

    public SearchScenario(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"the following customers")]
    public void GivenTheFollowingCustomers(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"the company name begins with the term ""(.*)""")]
    public void WhenTheCompanyNameBeginsWithTheTerm(string companyName)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"then (.*) record should be returned\.")]
    public void ThenThenRecordShouldBeReturned(int count)
    {
        ScenarioContext.StepIsPending();
    }
}