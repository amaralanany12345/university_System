using SchoolSystem.General;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;
using System.Security.Claims;

namespace SchoolSystem.Services
{
    public class CourseService:ICourse
    {
        AppDbContext context;
        UserService userService;
        GroupService groupService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CourseService(IHttpContextAccessor httpContextAccessor)
        {
            context = new AppDbContext();
            groupService = new GroupService();
            _httpContextAccessor = httpContextAccessor;
            userService = new UserService(_httpContextAccessor);
        }
        public Course addCourse(string courseName, string courseDescription,int teachetId, int groupId,int score){
            var course = new Course();
            course.courseName = courseName;
            course.description = courseDescription;
            course.score = score;

            var teacher = (from item in context.teachers where item.id == teachetId select item).FirstOrDefault();
            if(teacher == null)
            {
                throw new ArgumentException("teacher is not found");
            }
            course.teacher = teacher;
            course.teacherId = teacher.id;

            var group = (from item in context.groups where item.id == groupId select item).FirstOrDefault();
            if (group == null)
            {
                throw new ArgumentException("group is not found");
            }
            course.group = group;
            course.groupId = groupId;
            course.academicYear = group.academicYear;
            var studentsInGroup = (from item in context.students where item.groupId == groupId select item).ToList();
            foreach(var item in studentsInGroup)
            {
                var studentCourse = new StudentCourse();
                studentCourse.course = course;
                studentCourse.courseId = course.id;
                studentCourse.student = item;
                studentCourse.studentId = item.id;
                course.studentCourses.Add(studentCourse);
                context.studentCourses.Add(studentCourse);

                var studentCourseWork = new CourseWork();
                studentCourseWork.course = course;
                studentCourseWork.courseId = course.id;
                studentCourseWork.student = item;
                studentCourseWork.studentId = item.id;
                studentCourseWork.academicYear = item.academicYear;
                course.courseWorks.Add(studentCourseWork);
                context.courseWorks.Add(studentCourseWork);

                var studentAttendance = new Attendance();
                studentAttendance.course = course;
                studentAttendance.courseId = course.id;
                studentAttendance.student = item;
                studentAttendance.studentId = item.id;
                course.attendances.Add(studentAttendance);
                context.attendances.Add(studentAttendance);

            }
            context.courses.Add(course);
            context.SaveChanges();
            group.totalScoreOfTheYear = groupService.getTotalGroupScore(groupId);
            context.SaveChanges();
            return course;
        }
        public Course getCourse(int courseId){
            var course = (from item in context.courses where item.id == courseId select item).FirstOrDefault();
            if (course == null)
            {
                throw new ArgumentException("course is not found");
            }

            return course;
        }
        public void deleteCourse(int courseId) {
            var course = getCourse(courseId);
            var group = (from item in context.groups where item.id == course.groupId select item).FirstOrDefault();
            if(group == null)
            {
                throw new ArgumentException("group is not found");
            }
            context.courses.Remove(course);
            context.SaveChanges();
            group.totalScoreOfTheYear= groupService.getTotalGroupScore(group.id);
            context.SaveChanges();
        }

        public ICollection<Student> getCourseStudents(int courseId)
        {
            var teacher = context.teachers.FirstOrDefault(item => item.id == userService.getCurrentUserId());
            if (teacher == null)
            {
                throw new ArgumentException("Teacher is not found.");
            }

            var course =getCourse(courseId);
            var studentsInCourse = (from studentCourse in context.studentCourses join student in context.students
                on studentCourse.studentId equals student.id where studentCourse.courseId==courseId select student).ToList();
            return studentsInCourse;
        }

        public ICollection<Course> getStudentCourses(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if(student == null)
            {
                throw new ArgumentException("student is not found");
            }
            var studentCourses = (from studentCourse in context.studentCourses join course in context.courses
                on studentCourse.courseId equals course.id where studentCourse.studentId == studentId select course).ToList();
            return studentCourses;
        }
        public Course updateCourseScore(int courseId,int score)
        {
            var course = getCourse(courseId);
            course.score = score;
            context.SaveChanges();
            return course;
        }
    }
}
