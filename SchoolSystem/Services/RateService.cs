using SchoolSystem.Enums;

namespace SchoolSystem.Services
{
    public class RateService
    {
        public Rate determineRate(int score,int totalScore)
        {
            var rate = new Rate();
            if (((float)score / (float)totalScore) >= 0.85)
            {
                rate = Rate.A;
            }
            else if (((float)score / (float)totalScore) >= 0.75 && ((float)score / (float)totalScore) <0.85)
            {
                rate = Rate.B;
            }
            else if (((float)score / (float)totalScore) >= 0.65 && ((float)score / (float)totalScore) < 0.75)
            {
                rate = Rate.C;
            }
            else if (((float)score / (float)totalScore) >= 0.50 && ((float)score / (float)totalScore) < 0.65)
            {
                rate = Rate.D;
            }
            else if((float)score / (float)totalScore < 0.50)
            {
                rate = Rate.F;
            }
            Console.WriteLine((float)score / (float)totalScore);
            return rate;
        }
    }
}
