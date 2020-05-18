using MongoDB.Bson;

namespace Core
{
    public abstract class ParentEntity
    {
        private string _id;

        protected ParentEntity()
        {
            _id = ObjectId.GenerateNewId().ToString();
        }


        public string Id
        {
            get { return _id; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _id = ObjectId.GenerateNewId().ToString();
                else
                    _id = value;
            }
        }

    }
}
