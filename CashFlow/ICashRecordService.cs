using System.Data;
using System.ServiceModel;

namespace CashFlow
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "ICashRecordService" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface ICashRecordService
    {
        [OperationContract]
        string Register(string fullName, string email, string password);

        [OperationContract]
        string Login(string email, string password);

        [OperationContract]
        string AddRecord(string description, int amount, string email);

        [OperationContract]
        string DeleteRecord(int id);

        [OperationContract]
        string UpdateRecord(int id, string desc, int amount);

        [OperationContract]
        DataTable GetAllRecords(string email);

    }
}
