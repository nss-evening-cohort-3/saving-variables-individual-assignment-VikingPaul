using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using Moq;
using SavingVariables.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class VariableRepositoryTests
    {

        Mock<VariableContext> moq_context { get; set; }
        Mock<DbSet<Variables>> moq_uber { get; set; }
        List<Variables> Var_list { get; set; }
        VariableRepository repo { get; set; }

        public void ConnectMockToData()
        {
            var queryList = Var_list.AsQueryable();
            moq_uber.As<IQueryable<Variables>>().Setup(m => m.Provider).Returns(queryList.Provider);
            moq_uber.As<IQueryable<Variables>>().Setup(m => m.Expression).Returns(queryList.Expression);
            moq_uber.As<IQueryable<Variables>>().Setup(m => m.ElementType).Returns(queryList.ElementType);
            moq_uber.As<IQueryable<Variables>>().Setup(m => m.GetEnumerator()).Returns(() => queryList.GetEnumerator());
            moq_context.Setup(c => c.Vars).Returns(moq_uber.Object);
            moq_uber.Setup(c => c.Add(It.IsAny<Variables>())).Callback((Variables a) => Var_list.Add(a));
            moq_uber.Setup(c => c.Remove(It.IsAny<Variables>())).Callback((Variables a) => Var_list.Remove(a));
        }

        [TestInitialize]
        public void init()
        {
            moq_context = new Mock<VariableContext>();
            moq_uber = new Mock<DbSet<Variables>>();
            Var_list = new List<Variables>();
            ConnectMockToData();
            repo = new VariableRepository(moq_context.Object);
        }

        [TestMethod]
        public void RepoCanMakeInstance()
        {
            VariableRepository repo = new VariableRepository();
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        public void RepoCanSave()
        {
            Variables var = new Variables { Var = "x", Value = "6" };
            repo.Add(var);
            Variables newVar = repo.GetVar("x");
            Assert.AreSame(var, newVar);
        }
        [TestMethod]
        public void RepoCanGet()
        {
            Variables var = new Variables { Var = "x", Value = "6" };
            repo.Add(var);
            Variables newVar = repo.GetVar("x");
            Assert.AreEqual("6", newVar.Value);
        }
        [TestMethod]
        public void RepoCanDelete()
        {
            Variables var = new Variables { Var = "x", Value = "7" };
            Variables var2 = new Variables { Var = "y", Value = "8" };
            Variables var3 = new Variables { Var = "z", Value = "9" };
            repo.Add(var);
            repo.Add(var2);
            repo.Add(var3);
            Variables deletedVare = repo.Delete("x");
            Variables newVar = repo.GetVar("x");
            Assert.AreEqual(var, deletedVare);
            Assert.IsNull(newVar);
        }
    }
}
