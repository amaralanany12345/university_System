using SchoolSystem.General;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Services
{
    public class CourseWorkService:ICourseWork
    {
        UserService userService;
        AppDbContext context;
        RateService rateService;
        FinalResultService finalResultservice;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CourseWorkService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            userService = new UserService(_httpContextAccessor);
            context = new AppDbContext();
            rateService = new RateService();
            finalResultservice = new FinalResultService();
        }

        public CourseWork updateStudentCoureScore(int courseId, int studentId, int score)
        {
            var course = (from item in context.courses where item.id == courseId select item).FirstOrDefault();
            if (course == null)
            {
                throw new ArgumentException("student is not found");
            }
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if (student == null)
            {
                throw new ArgumentException("student is not found");
            }
            var courseWork = (from item in context.courseWorks where item.courseId == courseId && item.studentId == studentId select item).FirstOrDefault();
            if (courseWork == null)
            {
                throw new ArgumentException("courseWork is not found");
            }
            if (course.teacherId != userService.getCurrentUserId())
            {
                throw new ArgumentException("you can't put the score to the student");
            }
            if (score > course.score)
            {
                throw new ArgumentException("your score is bigger than the ecpected");
            }
            courseWork.studentScore = score;
            courseWork.studentCourseRate = rateService.determineRate(courseWork.studentScore,course.score);
            context.SaveChanges();
            return courseWork;
        }

        public List<CourseWork> getCourseWorks(int courseId)
        {
            var course = (from item in context.courses where item.id == courseId select item).FirstOrDefault();
            if (course == null)
            {
                throw new ArgumentException("course is not found");
            }
            if (course.teacherId != userService.getCurrentUserId())
            {
                throw new ArgumentException("you are not allowed to see the work of the courses");
            }
            var courseWorks = (from item in context.courseWorks where item.courseId == courseId select item).ToList();
            return courseWorks;
        }

        public List<CourseWork> getStudentCoursesWorks(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if (student == null)
            {
                throw new ArgumentException("student is not found");
            }

            if (studentId !=userService.getCurrentUserId())
            {
                throw new ArgumentException("you are not allowed see the student course work");
            }

            var studentCourseWorks = (from item in context.courseWorks where item.studentId == studentId select item).ToList();
            return studentCourseWorks;
        }

        public CourseWork remakeCourseWork(int studentId,int courseId,int newScore)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if(student == null)
            {
                throw new ArgumentException("student is not found");
            }
            var course = (from item in context.courses where item.id == courseId select item).FirstOrDefault();
            if (course == null)
            {
                throw new ArgumentException("course is not found");
            }
            var courseWrok = (from item in context.courseWorks where item.studentId == studentId && item.courseId == courseId select item).FirstOrDefault();
            if (courseWrok == null)
            {
                throw new ArgumentException("course Work is not found");
            }
            courseWrok.studentScore = newScore;
            courseWrok.studentCourseRate = rateService.determineRate(newScore,course.score);
            finalResultservice.getStudentTotalScore(studentId);
            finalResultservice.updateStudentState(studentId);
            if (courseWrok.studentCourseRate != Enums.Rate.F)
            {
                var failedCourse = (from item in context.failedCourses where item.studentId == studentId && item.courseId == courseId select item).FirstOrDefault();
                if (failedCourse == null)
                {
                    throw new ArgumentException("failed course is not found");
                }
                context.failedCourses.Remove(failedCourse);
                context.SaveChanges();
            }
            context.SaveChanges();
            return courseWrok;
        }
    }
}
