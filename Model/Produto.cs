using Google.Cloud.Firestore;

namespace PetDelivey.WebAPI.Model
{
    [FirestoreData]
    public class Produto
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public double Price { get; set; }

        [FirestoreProperty]
        public string urlImg { get; set; }

        [FirestoreProperty]
        public string descricaoImg { get; set; }

        [FirestoreProperty]
        public string descricaoProduto { get; set; }
    }
}
