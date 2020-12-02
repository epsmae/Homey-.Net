using System;

namespace Homey.Net.Sample.ViewModel
{
    public class YesNoDialogViewModel : ViewModelBase
    {
        private readonly string _titleText;
        private readonly string _descriptionText;
        private readonly string _positiveButtonText;
        private readonly string _negativeButtonText;
        private readonly Action _positiveAction;
        private readonly Action _negativeAction;
        private readonly bool _positiveButtonVisibility;
        private readonly bool _negativeButtonVisibility;

        public YesNoDialogViewModel(string titleText, string descriptionText, string positiveButtonText, string negativeButtonText, Action positiveAction, Action negativeAction)
        {
            _titleText = titleText;
            _descriptionText = descriptionText;
            _positiveButtonText = positiveButtonText;
            _negativeButtonText = negativeButtonText;
            _positiveAction = positiveAction;
            _negativeAction = negativeAction;
            _positiveButtonVisibility = !string.IsNullOrEmpty(positiveButtonText);
            _negativeButtonVisibility = !string.IsNullOrEmpty(negativeButtonText);
        }

        public string NegativeButtonText
        {
            get
            {
                return _negativeButtonText;
            }
        }

        public string PositiveButtonText
        {
            get
            {
                return _positiveButtonText;
            }
        }

        public bool PositiveButtonVisibility
        {
            get
            {
                return _positiveButtonVisibility;
            }
        }

        public bool NegativeButtonVisibility
        {
            get
            {
                return _negativeButtonVisibility;
            }
        }

        public string TitleText
        {
            get
            {
                return _titleText;
            }
        }

        public string DescriptionText
        {
            get
            {
                return _descriptionText;
            }
        }

        public void NegativeAction()
        {
            if (_negativeAction != null)
            {
                _negativeAction();
            }
        }

        public void PositiveAction()
        {
            if (_positiveAction != null)
            {
                _positiveAction();
            }
        }
    }
}
