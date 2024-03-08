using MOHU.Integration.Contracts.Interface;

namespace MOHU.Integration.Infrastructure.Service
{
    public class CorrelationIdService : ICorrelationIdService
    {
        private Guid? _id;
        public Guid GenerateCorrelationId()
        {
            if (_id is null)
                _id = Guid.NewGuid();
            return _id.Value;
        }

        public Guid GetCorrelationId()
        {
            var id = _id;
            _id = Guid.NewGuid();
            return id.Value;

        }
    }
}
