using System;

namespace UI.ResultScreen
{
    public class UIResultScreenController : BaseController<UIResultScreenModel>
    {
        public string ParseTimer(float timeInSeconds)
        {
            var time = TimeSpan.FromSeconds(timeInSeconds);
            var displayTime = time.ToString(@"hh\:mm\:ss");
            return displayTime;
        }
    }
}