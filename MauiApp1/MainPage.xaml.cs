namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly ICustomersService _customersService;
        private readonly ILocalControlService _localControlService;
        public MainPage(ICustomersService customersService,
                        ILocalControlService localControlService)
        {
            InitializeComponent();
            _customersService = customersService;
            _localControlService = localControlService;
            RefreshAsync();
        }

        async Task RefreshAsync()
        {
            var controls = await _localControlService.GetAllAsync();
            TestList.ItemsSource = controls.Select(x => x.ControlName).ToList();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            await _localControlService.AddAsync(new CardInfo { ControlName = $"Test {count}" });
            await RefreshAsync();
        }

        private async void CustomerBtn_Clicked(object sender, EventArgs e)
        {

            string answer = await DisplayPromptAsync("Hello", "What's your name?", placeholder: "Type your name");
            if (answer != null)
            {
                await _customersService.AddAsync(new CustomerInfo { Name = answer });
                await DisplayAlert("Welcome", $"Hello, {answer}", "Cancel");
            }
        }

        private async void ShowAllCustomers_Clicked(object sender, EventArgs e)
        {
            var customers = await _customersService.GetAllAsync();
            string answer = await DisplayActionSheet("See all customers", "Cancel", null, customers.Select(x => x.Name).ToArray());
            if (answer != "Cancel")
            {
                await DisplayAlert("Answer", $"{answer} is great!", "OK");
            }
        }
    }

}
