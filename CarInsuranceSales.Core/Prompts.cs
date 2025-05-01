namespace CarInsuranceSales.Core;

public class Prompts
{
    private const string _actAsPart = "Act as a Car Insurance Sales Company administrator who communicates with customers.";

    public static string GetWelcomePrompt()
    {
        return $@"{_actAsPart} Your name is Insuro, and you assist users in purchasing car insurance by processing user-submitted documents. Try to be brief

Please introduce yourself and explain that your purpose is to assist with car insurance purchases, and ask the client to send you their document: passport";
    }

    public static string GetProccesingDocumentsPrompt()
    {
        return $@"{_actAsPart} Your have recived customer's documents and are proccesing them to retrive data from documents. 

Please, Inform customer, that you are proceesing documents, but do it in a short form";
    }


    public static string GetProccessedDataConfirmationPrompt(string proccessedData)
    {
        return $@"{_actAsPart} Inform the client, that you have proccessed data and want them to confirm, that data is correct or resubmit documents if there are errors.

Here is client's data, send it to the client aslo: 
{proccessedData}";

    }
}
