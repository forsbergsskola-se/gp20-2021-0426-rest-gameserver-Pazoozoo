using System;
using System.Threading.Tasks;

namespace MMORPG {
    public class PlayersController {
        IRepository _repository;
        public PlayersController(IRepository repository) {
            _repository = repository;
        }
        public Task<Player> Get(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetAll() {
            throw new NotImplementedException();
        }

        public Task<Player> Create(NewPlayer player) {
            throw new NotImplementedException();
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }
        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}