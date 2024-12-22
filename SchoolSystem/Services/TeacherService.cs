using SchoolSystem.General;
using SchoolSystem.Models;

namespace SchoolSystem.Services
{
    public class TeacherService:SigningService
    {
        AppDbContext context;
        private readonly Jwt _jwt;
        public TeacherService(Jwt jwt) : base(jwt)
        {
            _jwt = jwt;
            context = new AppDbContext();

        }
        public override SigningResponse signin(SigninInfo userInfo)
        {
            var teacher = (from item in context.teachers where item.email == userInfo.email select item).FirstOrDefault();
            if (teacher == null || !(verifyPassword(userInfo.password, teacher.password)))
            {
                throw new ArgumentException("teacher is not found");
            }
            return new SigningResponse
            {
                user = teacher,
                token = generateToken(teacher),
            };
        }

        public SigningResponse registerTeacher(SignUpInfo userInfo)
        {
            var teacher = new Teacher();
            teacher.userName = userInfo.name;
            teacher.email = userInfo.email;
            teacher.password = hashPassword(userInfo.password);
            context.teachers.Add(teacher);
            context.SaveChanges();

            return new SigningResponse
            {
                user = teacher,
                token = generateToken(teacher),
            };
        }
        public Teacher getTeacher(int teacherId)
        {
            var teacher = (from item in context.teachers where item.id == teacherId select item).FirstOrDefault();
            if (teacher == null)
            {
                throw new ArgumentException("student is not found");
            }
            return teacher;
        }

        public void deleteTeacher(int studentId)
        {
            var teacher = getTeacher(studentId);
            context.teachers.Remove(teacher);
            context.SaveChanges();
        }
    }
}
