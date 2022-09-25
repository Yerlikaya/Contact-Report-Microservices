using AutoMapper;
using Contact.Service.Dtos;
using Contact.Service.Models;
using Contact.Service.Settings;
using MongoDB.Driver;
using Shared.Dtos;
using System.Linq.Expressions;

namespace Contact.Service.Services
{
    public class CommunicationService: ICommunicationService
    {
        #region private
        private readonly IMongoCollection<Communication> _communicationCollection;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public CommunicationService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _communicationCollection = database.GetCollection<Communication>(databaseSettings.CommunicationCollectionName);

            _mapper = mapper;
        }
        #endregion

        #region methods
        public async Task<Response<List<CommunicationDto>>> GetAllAsync()
        {
            var communications = await _communicationCollection.Find(x => true).ToListAsync();
            return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
        }
        public async Task<Response<List<CommunicationDto>>> GetAllByContactId(string contactId)
        {
            var communications = await _communicationCollection.Find(x => x.ContactId == contactId).ToListAsync();
            return Response<List<CommunicationDto>>.Success(_mapper.Map<List<CommunicationDto>>(communications), 200);
        }
        public async Task<Response<CommunicationDto>> GetById(string id)
        {
            var communication = await _communicationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if(communication == null)
            {
                return Response<CommunicationDto>.Fail("Communication not found!", 404);
            }
            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(communication), 200);
        }
        public async Task<Response<CommunicationDto>> CreateAsync(CommunicationCreateDto communication)
        {
            var newCommunication = _mapper.Map<Communication>(communication);
            await _communicationCollection.InsertOneAsync(newCommunication);
            return Response<CommunicationDto>.Success(_mapper.Map<CommunicationDto>(newCommunication), 200);
        }
        public async Task<Response<NoContent>> UpdateAsync(CommunicationUpdateDto communication)
        {
            var updateCoummunication = _mapper.Map<Communication>(communication);
            var result = await _communicationCollection.FindOneAndReplaceAsync(x => x.Id == updateCoummunication.Id, updateCoummunication);
            if(result == null)
            {
                return Response<NoContent>.Fail("Communications not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _communicationCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Fail("Communications not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }
        #endregion
    }
}
