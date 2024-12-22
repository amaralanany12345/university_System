using SchoolSystem.Enums;
using SchoolSystem.General;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Services
{
    public class FinalResultService : IFinalResult
    {
        AppDbContext context;
        RateService rateService;
        public FinalResultService()
        {
            context = new AppDbContext();
            rateService = new RateService();
        }
        public FinalResult createFinalResult(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if(student == null)
            {
                throw new ArgumentException("student is not found");
            }
            var studentGroup = (from item in context.groups where item.students.Contains(student) select item).FirstOrDefault();
            if (studentGroup == null)
            {
                throw new ArgumentException("student group is not found");
            }
            var result = new FinalResult();
            context.finalResults.Add(result);
            context.SaveChanges();

            result.student = student;
            result.studentId = studentId;
            result.academicYear = student.academicYear;
            result.totalScore = studentGroup.totalScoreOfTheYear;

            result.finalScore=getStudentTotalScore(studentId);
            updateStudentState(studentId);
            context.SaveChanges();
            return result;
        }

        public void deleteFinalResult(int studentId)
        {
            var finalResult = (from item in context.finalResults where item.studentId == studentId  select item).FirstOrDefault();
            if(finalResult == null)
            {
                throw new ArgumentException("final result is not found");
            }
            context.finalResults.Remove(finalResult);
            context.SaveChanges();
        }
        public FinalResult getStudentFinalResult(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if (student == null)
            {
                throw new ArgumentException("student is not found");
            }
            var finalResult = (from item in context.finalResults where item.studentId == studentId && item.academicYear==student.academicYear select item).FirstOrDefault();
            if(finalResult == null)
            {
                throw new ArgumentException("student final result is not found");
            }
            return finalResult;
        }

        public int getStudentTotalScore(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if (student == null)
            {
                throw new ArgumentException("student is not found");
            }
            var result = (from item in context.finalResults where item.studentId == studentId && item.academicYear == student.academicYear  select item).FirstOrDefault();
            if (result== null)
            {
                throw new ArgumentException("student final result is not found");
            }
            var studentCoursesWorks = (from item in context.courseWorks where item.studentId == studentId select item).ToList();
            var resultFinalScore = 0;
            foreach (var item in studentCoursesWorks)
            {
                var course = (from i in context.courses where i.id == item.courseId select i).FirstOrDefault();
                if (course == null)
                {
                    throw new ArgumentException("course is not found");
                }
                if (item.studentCourseRate == Rate.F)
                {
                    var failedCourse = new FailedCourse();
                    failedCourse.student = student;
                    failedCourse.studentId = studentId;
                    failedCourse.course = course;
                    failedCourse.courseId = course.id;
                    failedCourse.finalResult = result;
                    failedCourse.finalResultId = result.id;
                    failedCourse.studentCourseRate = item.studentCourseRate;
                    failedCourse.studentCourseScore = item.studentScore;
                    result.failedCourses.Add(failedCourse);
                    context.failedCourses.Add(failedCourse);
                }
                resultFinalScore += item.studentScore;
            }
            result.finalScore = resultFinalScore;
            result.finalRate = rateService.determineRate(result.finalScore, result.totalScore);
            context.SaveChanges();
            return resultFinalScore;
        }

        public FinalResult updateStudentState(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if(student is null)
            {
                throw new ArgumentException("student is not found");
            }
            var studentResult = (from item in context.finalResults where item.studentId == studentId && item.academicYear == student.academicYear  select item).FirstOrDefault();
            if(studentResult is null)
            {
                throw new ArgumentException("student result is not found");
            }
            var studentFailedcourses = (from item in context.failedCourses where item.studentId == studentId select item).ToList();
            if (studentFailedcourses.Count > 0)
            {
                student.academicYear = student.academicYear;
            }
            else
            {
                student.academicYear++;
                var newStudentGroup = (from item in context.groups where item.academicYear == student.academicYear select item).FirstOrDefault();
                if (newStudentGroup == null)
                {
                    throw new ArgumentException("group is not found");
                }
                student.group = newStudentGroup;
                student.groupId = newStudentGroup.id;
            }
            context.SaveChanges();
            return studentResult;
        }


    }
}
