using SchoolSystem.Models;

namespace SchoolSystem.Interfaces
{
    public interface IFinalResult
    {
        FinalResult createFinalResult(int studentId);
        FinalResult getStudentFinalResult(int studentId);
        void deleteFinalResult(int studentId);
        FinalResult updateStudentState(int studentId);
        int getStudentTotalScore(int studentId);
    }
}
