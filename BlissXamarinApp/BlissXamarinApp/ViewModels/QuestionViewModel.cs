using System.Threading.Tasks;
using BlissXamarinApp.Models;
using System;
using BlissXamarinApp.Interfaces;
using Xamarin.Forms;

namespace BlissXamarinApp.ViewModels
{
    public class QuestionViewModel : BaseViewModel
    {
        private readonly IBlissApiService _blissXamarinApiService;
        private readonly Question _question;
        public Question Question { get; set; }

        public Command<QuestionChoice> AnswerCommand { get; }
        public Command ShareCommand { get; }

        public QuestionViewModel(IBlissApiService blissXamarinApiService, Question question)
        {
            try
            {
                _blissXamarinApiService = blissXamarinApiService;
                Question = new Question();

                Question = question;
                _question = question;

                AnswerCommand = new Command<QuestionChoice>(ExecuteAnswerCommand);
                ShareCommand = new Command(ExecuteShareCommand);

                Title = $"Question {question.Id}";
            }
            catch (Exception e)
            {
                
            }
        }

        public override async Task LoadAsync()
        {
            try
            {
                IsBusy = true;
                Question = await _blissXamarinApiService.GetQuestionByIdAsync(_question.Id);
                //OnPropertyChanged(nameof(Question));
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

        private async void ExecuteShareCommand()
        {
            await PushAsync<ShareViewModel>($"?question_id={_question.Id}");
        }

        private async void ExecuteAnswerCommand(QuestionChoice choice)
        {
            if (await DisplayAlert("Vote", $"Do you want to vote for '{choice.Choice}'?", "YES", "NO"))
            {
                //Call API to register vote
                await DisplayAlert("Voted successfully", $"You voted in '{choice.Choice}'! Thanks", "OK");
            }
        }
    }
}
