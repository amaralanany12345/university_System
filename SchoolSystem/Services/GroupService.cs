using SchoolSystem.General;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;

namespace SchoolSystem.Services
{
    public class GroupService : IGroup
    {
        AppDbContext context;
        public GroupService()
        {
            context = new AppDbContext();
        }
        public Group addGroup(AcademicYear academicYear)
        {
            var group = new Group();
            group.academicYear = academicYear;
            context.groups.Add(group);
            context.SaveChanges();
            return group;
        }

        public Group addStduentToGroup(int groupId, int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if(student== null)
            {
                throw new ArgumentException("stduent is not found");
            }
            var group = getGroup(groupId);
            group.students.Add(student);
            context.SaveChanges();
            return group;

        }

        public void deleteGroup(int groupId)
        {
            var group = getGroup(groupId);
            context.groups.Remove(group);
            context.SaveChanges();
        }

        public Group getGroup(int groupId)
        {
            var group = context.groups.Where(a => a.id == groupId).FirstOrDefault();
            if(group == null)
            {
                throw new ArgumentException("group is not found");
            }
            return group;
        }

        public ICollection<Course> getGroupCourses(int groupId)
        {
            var groupCourses = (from item in context.courses where item.groupId == groupId select item).ToList();
            return groupCourses;
        }

        public ICollection<Student> getGroupStudents(int groupId)
        {
            var groupStudent = (from item in context.students where item.groupId == groupId select item).ToList();
            return groupStudent;
        }

        public int getTotalGroupScore(int groupId)
        {
            var group = (from item in context.groups where item.id == groupId select item).FirstOrDefault();
            if (group == null)
            {
                throw new ArgumentException("group is not found");
            }

            var groupCourses = (from item in context.courses where item.groupId == groupId select item).ToList();
            var totalScore = 0;
            foreach (var item in groupCourses)
            {
                totalScore += item.score;
            }
            return totalScore;
        }

    }
}
