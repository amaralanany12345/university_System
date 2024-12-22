using SchoolSystem.General;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Services
{
    public class StudentService : SigningService
    {
        AppDbContext context;
        private readonly Jwt _jwt;
        public StudentService(Jwt jwt): base(jwt) 
        {
            _jwt = jwt;
            context = new AppDbContext();

        }
        public override SigningResponse signin(SigninInfo userInfo)
        {
            var student = (from item in context.students where item.email == userInfo.email select item).FirstOrDefault();
            if(student == null || !(verifyPassword(userInfo.password, student.password)))
            {
                throw new ArgumentException("user is not found");
            }
            return new SigningResponse
            {
                user = student,
                token = generateToken(student),
            };
        }

        public SigningResponse registerStudent(SignUpInfo userInfo,int groupId)
        {
            var student = new Student();
            student.userName = userInfo.name;
            student.email = userInfo.email;
            student.password = hashPassword(userInfo.password);
            var group = (from item in context.groups where item.id == groupId select item).FirstOrDefault();
            if (group == null)
            {
                throw new ArgumentException("group is not found");
            }
            student.group = group;
            student.groupId = groupId;
            student.academicYear = group.academicYear;
            context.students.Add(student);
            context.SaveChanges();
            var coursesInGroup = (from item in context.courses where item.groupId == groupId select item).ToList();
            foreach (var item in coursesInGroup)
            {
                Console.WriteLine(item.id);
                var studentCourse = new StudentCourse();
                studentCourse.course = item;
                studentCourse.courseId = item.id;
                studentCourse.student = student;
                studentCourse.studentId = student.id;
                item.studentCourses.Add(studentCourse);
                context.studentCourses.Add(studentCourse);

                var studentCourseWork = new CourseWork();
                studentCourseWork.course = item;
                studentCourseWork.courseId =item.id;
                studentCourseWork.student = student;
                studentCourseWork.studentId = student.id;
                studentCourseWork.academicYear = student.academicYear;
                item.courseWorks.Add(studentCourseWork);
                context.courseWorks.Add(studentCourseWork);

                var studentAttendance = new Attendance();
                studentAttendance.course = item;
                studentAttendance.courseId = item.id;
                studentAttendance.student =   student;
                studentAttendance.studentId = student.id;
                item.attendances.Add(studentAttendance);
                context.attendances.Add(studentAttendance);
            }
            context.SaveChanges();

            return new SigningResponse
            {
                user=student,
                token=generateToken(student),
            };

        }
        public Student getStudent(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if(student == null)
            {
                throw new ArgumentException("student is not found");
            }
            return student;
        }
        public void deleteStudent(int studentId)
        {
            var student = getStudent(studentId);
            context.students.Remove(student);
            context.SaveChanges();
        }

        public Student updateStudent(int studentId,AcademicYear studentAcademicYear,int groupId)
        {
            var student = getStudent(studentId);
            student.groupId = groupId;
            student.academicYear = studentAcademicYear;
            context.SaveChanges();
            return student;
        }
    }
}
