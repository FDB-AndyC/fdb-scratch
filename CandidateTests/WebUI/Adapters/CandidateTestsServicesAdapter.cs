namespace WebUI.Adapters
{
    using global::Adapters;

    using ServiceContracts;

    public class CandidateTestsServicesAdapter : IServicesAdapter, ICandidateServiceAdapter
    {
        #region Members

        private readonly ICandidateService candidateService;

        #endregion // members

        #region Construction

        public CandidateTestsServicesAdapter(ICandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        #endregion // construction

        #region Properties

        ICandidateServiceAdapter IServicesAdapter.Candidate => this;

        #endregion // properties

        #region ICandidateServicesAdapter

        #endregion // ICandidateServicesAdapter
    }
}
