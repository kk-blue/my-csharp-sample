﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Abp.Domain.Uow;

using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;

using B2000_AbpEf.Model;


namespace B2000_AbpEf.Test
{


    public class AbpTest : ITransientDependency
    {

        /// <summary>
        /// 学校存储
        /// </summary>
        private readonly IRepository<School, Int32> _schoolRepository;

        /// <summary>
        /// 教师存储
        /// </summary>
        private readonly IRepository<Teacher, Int32> _teacherRepository;


        public AbpTest(IRepository<School, Int32> schoolRepository, IRepository<Teacher, Int32> teacherRepository)
        {
            _schoolRepository = schoolRepository;
            _teacherRepository = teacherRepository;
        }



        /// <summary>
        /// 用于测试的学校名称.
        /// </summary>
        private const string TEST_SCHOOL_NAME = "TEST_SCHOOL_ABP";



        /// <summary>
        /// 单表基本操作测试.
        /// </summary>
        public void TestOneTableFunc()
        {
            Console.WriteLine("----- ABP test one table func -----");

            // 测试插入.
            Console.WriteLine("-- INSERT --");

            School mySchool = new School()
            {
                SchoolName = TEST_SCHOOL_NAME,
                SchoolAddress = "上海市某某路1234号"
            };
            var schoolID = this._schoolRepository.InsertAndGetId(mySchool);
            Console.WriteLine("After InsertAndGetId, School id = {0}", schoolID);




            Console.WriteLine("-- SELECT BY KEY --");
            // 根据 id 进行查询.
            var mySchool2 = this._schoolRepository.Get(schoolID);
            printSchool(mySchool2);


            Console.WriteLine("-- SELECT BY NAME --");
            // 根据名称进行查询.
            var mySchool3 = this._schoolRepository.FirstOrDefault(p => p.SchoolName == TEST_SCHOOL_NAME);
            printSchool(mySchool3);



            Console.WriteLine("-- DELETE --");
            this._schoolRepository.Delete(schoolID);


            // 注意： 数据删除后， 用 this._schoolRepository.Get(schoolID); 会抛异常的.

            mySchool3 = this._schoolRepository.FirstOrDefault(p => p.SchoolName == TEST_SCHOOL_NAME);
            if (mySchool3 == null)
            {
                Console.WriteLine("Success!  After delete.  School Data is Not Found!");
            }

            Console.WriteLine();
        }


        private void printSchool(School mySchool)
        {
            Console.WriteLine("SCHOOL : id = {0}, name = {1}, address = {2}", mySchool.Id, mySchool.SchoolName, mySchool.SchoolAddress);
        }




        /// <summary>
        /// 多表的基本操作测试.
        /// </summary>
        public void TestMulTableFunc()
        {
            Console.WriteLine("----- ABP test mul table func -----");

            // 测试插入.
            Console.WriteLine("-- INSERT SCHOOL --");

            School mySchool = new School()
            {
                SchoolName = TEST_SCHOOL_NAME,
                SchoolAddress = "上海市某某路1234号"
            };
            var schoolID = this._schoolRepository.InsertAndGetId(mySchool);


            // 测试插入老师.
            Console.WriteLine("-- INSERT TEACHER --");

            Teacher t1 = new Teacher()
            {
                SchoolID = schoolID,
                TeacherName = "张三"
            };
            Teacher t2 = new Teacher()
            {
                SchoolID = schoolID,
                TeacherName = "李四"
            };

            var teacherID1 = this._teacherRepository.InsertAndGetId(t1);
            var teacherID2 = this._teacherRepository.InsertAndGetId(t2);




            Console.WriteLine("-- SELECT --");
            // 根据 id 进行查询.
            var mySchool2 = this._schoolRepository.Get(schoolID);
            printSchool(mySchool2);

            var myTeacher = this._teacherRepository.Get(teacherID1);            
            Console.WriteLine("  TEACHER : id = {0}, name = {1}.", myTeacher.Id, myTeacher.TeacherName);
            myTeacher = this._teacherRepository.Get(teacherID2);
            Console.WriteLine("  TEACHER : id = {0}, name = {1}.", myTeacher.Id, myTeacher.TeacherName);



            // 测试删除.
            Console.WriteLine("-- DELETE SCHOOL & TEACHER --");
            
            this._teacherRepository.Delete(teacherID1);
            this._teacherRepository.Delete(teacherID2);

            this._schoolRepository.Delete(schoolID);



            var mySchool3 = this._schoolRepository.FirstOrDefault(p => p.SchoolName == TEST_SCHOOL_NAME);
            if (mySchool3 == null)
            {
                Console.WriteLine("Success!  After delete.  School Data is Not Found!");
            }

            Console.WriteLine();

        }




        /// <summary>
        /// 翻页的测试.
        /// </summary>
        [UnitOfWork]
        public void TestPage()
        {

            Console.WriteLine("----- ABP test Page func -----");

            // 测试插入 20 行.
            Console.WriteLine("-- INSERT  20 LINE --");


            List<int> schoolIDList = new List<int>();

            for (int i = 1; i <= 20; i++)
            {
                School mySchool = new School()
                {
                    SchoolName = String.Format("{0}_{1:000}", TEST_SCHOOL_NAME, i),
                    SchoolAddress = String.Format("上海市某某路{0:000}号", i)
                };
                var schoolID = this._schoolRepository.InsertAndGetId(mySchool);
                schoolIDList.Add(schoolID);
            }


            Console.WriteLine("-- PAGE --");

            // 排序 = 名称逆序
            // 跳过10行， 取5行.
            var query = this._schoolRepository.Query(p=>p.Where(d=>d.SchoolName.StartsWith(TEST_SCHOOL_NAME)))
                .OrderByDescending(o => o.SchoolName).Skip(10).Take(5);


            // 这里好像会出错...
            var resultList = query.ToList();

            Console.WriteLine("Skip 10 Take 5 Result !");
            foreach (var mySchool in resultList)
            {
                printSchool(mySchool);
            }



            Console.WriteLine("-- DELETE --");

            foreach(var sid in schoolIDList)
            {
                this._schoolRepository.Delete(sid);
            }


            Console.WriteLine();
        }



    }
}