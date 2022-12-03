namespace ShoppingLikeFiles.DomainServices.Service;

class PaymentService : IPaymentService
{
    private readonly IDataService dataService;
    //private readonly ILogger logger;

    public PaymentService(IDataService dataService)//, ILogger logger)
    {
        this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        //logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> BuyItemAsync(int userId, int price, int caffId)
    {
        //logger.Verbose("Called {method} with args: {userId}, {price}, {caffId}", nameof(BuyItemAsync), userId, price, caffId);
        if (price < 0)
            return false;
        CaffDTO caffDTO = await this.dataService.GetCaffAsync(caffId);
        if (caffDTO == null)
            return false;
        return true;
    }
}
