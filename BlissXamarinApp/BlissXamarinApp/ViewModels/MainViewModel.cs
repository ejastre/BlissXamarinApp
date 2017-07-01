using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BlissXamarinApp.Interfaces;
using BlissXamarinApp.Models;
using BlissXamarinApp.Utils;
using Xamarin.Forms;

namespace BlissXamarinApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IBlissApiService _blissXamarinApiService;

        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                SetProperty(ref _searchTerm, value);
                SearchCommand.ChangeCanExecute();
                SearchCommandClear(_searchTerm);
            }
        }

        public ObservableCollection<Question> Questions { get; set; }
        private ObservableCollection<Question> QuestionsAll { get; }

        public Command ShareCommand { get; }
        public Command SearchCommand { get; }
        public Command<Question> ShowQuestionCommand { get; }
        public Command ForceRefreshCommand { get; }
        public Command LoadMoreCommand { get; }

        public MainViewModel(IBlissApiService blissXamarinApiService)
        {
            try
            {
                Title = "Bliss Test";

                _blissXamarinApiService = blissXamarinApiService;
                Questions = new ObservableCollection<Question>();
                QuestionsAll = new ObservableCollection<Question>();

                SearchCommand = new Command(ExecuteSearchCommand);
                ShareCommand = new Command(ExecuteShareCommand);
                ForceRefreshCommand = new Command(ExecuteForceRefreshCommand);
                LoadMoreCommand = new Command(ExecuteLoadMoreCommand);
                ShowQuestionCommand = new Command<Question>(ExecuteShowQuestionCommand);
            }
            catch (Exception e)
            {

            }
        }

        private async void ExecuteShareCommand()
        {
            Questions.Clear();
            await PushAsync<ShareViewModel>("");
        }

        private async void ExecuteForceRefreshCommand()
        {
            Questions.Clear();
            await LoadAsync();
        }

        private async void ExecuteLoadMoreCommand()
        {
            await LoadAsync();
        }

        private async void ExecuteShowQuestionCommand(Question question)
        {
            Questions.Clear();
            await PushAsync<QuestionViewModel>(question);
        }

        private void SearchCommandClear(string searchTerm)
        {
            Questions.Clear();

            if (QuestionsAll.Count > 0 && string.IsNullOrEmpty(searchTerm))
            {
                Questions = QuestionsAll;
                OnPropertyChanged(nameof(Questions));
            }
        }

        private async void ExecuteSearchCommand()
        {
            try
            {
                IsBusy = true;
                
                var questions = await _blissXamarinApiService.GetQuestionsByFilterAsync(Questions.Count, SearchTerm);

                if (questions != null && questions.Count > 0)
                {
                    IsEmpty = false;
                    
                    foreach (var question in questions)
                    {
                        Questions.Add(question);
                    }
                    OnPropertyChanged(nameof(Questions));
                }
            }
            catch (Exception e)
            {
                IsEmpty = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override async Task LoadAsync()
        {
            try
            {
                if (Util.CheckConnectivity())
                {
                    IsBusy = true;
                    var questions = await _blissXamarinApiService.GetQuestionsAsync(Questions.Count);

                    if (questions != null && questions.Count > 0)
                    {
                        foreach (var question in questions)
                        {
                            Questions.Add(question);
                            QuestionsAll.Add(question);
                        }
                        OnPropertyChanged(nameof(Questions));
                    }
                    else
                        IsEmpty = true;
                }
            }
            catch (Exception e)
            {
                IsEmpty = true;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
