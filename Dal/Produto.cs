using PetDelivey.WebAPI.Dal.Repositories;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace PetDelivey.WebAPI.Dal
{
    public class Produto : IProduto
    {
        string projectId;
        FirestoreDb fireStoreDb;

        public Produto()
        {
            string arquivoApiKey = @"petdeliveryapi-firebase-adminsdk-3ddxj-215c84fd3e.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", arquivoApiKey);
            projectId = "petdeliveryapi";
            fireStoreDb = FirestoreDb.Create(projectId);
        }

        public string AddProduto(Model.Produto produto)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("produtos");
                var id = colRef.AddAsync(produto).Result.Id;
                var shardRef = colRef.Document(id.ToString());
                shardRef.UpdateAsync("Id", id);
                return id;
            }
            catch
            {
                return "Error";
            }
        }

        public async void DeleteProduto(string id)
        {
            try
            {
                DocumentReference produtoRef = fireStoreDb.Collection("produtos").Document(id);
                await produtoRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Model.Produto> GetProdutoId(string id)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.Collection("produtos").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    Model.Produto produto = snapshot.ConvertTo<Model.Produto>();
                    produto.Id = snapshot.Id;
                    return produto;
                }
                else
                {
                    return new Model.Produto();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Model.Produto>> GetProdutos()
        {
            try
            {
                Query produtoQuery = fireStoreDb.Collection("produtos");
                QuerySnapshot inscricaoQuerySnaphot = await produtoQuery.GetSnapshotAsync();
                List<Model.Produto> listaProdutos = new List<Model.Produto>();
                foreach (DocumentSnapshot documentSnapshot in inscricaoQuerySnaphot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(city);
                        Model.Produto novoProduto = JsonConvert.DeserializeObject<Model.Produto>(json);
                        novoProduto.Id = documentSnapshot.Id;
                        listaProdutos.Add(novoProduto);
                    }
                }
                List<Model.Produto> listaProdutoOrdenada = listaProdutos.OrderBy(x => x.Name).ToList();
                return listaProdutoOrdenada;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                throw;
            }

        }

        public async void UpdateProduto(Model.Produto produto)
        {
            try
            {
                DocumentReference produtoRef = fireStoreDb.Collection("produtos").Document(produto.Id);
                await produtoRef.SetAsync(produto, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
    }
}