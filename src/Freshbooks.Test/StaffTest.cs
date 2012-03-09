﻿#region Copyright (c) 2012 SmartVault, Inc.
//  Permission is hereby granted, free of charge, to any person obtaining a copy 
//  of this software and associated documentation files (the "Software"), to deal 
//  in the Software without restriction, including without limitation the rights 
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
//  copies of the Software, and to permit persons to whom the Software is 
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in 
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
//  IN THE SOFTWARE.
#endregion
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Freshbooks.Library.Model;

namespace Freshbooks.Test
{
    [TestClass]
    public class StaffTest
    {
        static IStaffService Service { get { return AuthenticationTest.Default.Staff; } }

        private static StaffMember _self;
        internal static StaffMember Self { get { return _self ?? (_self = Service.Current().Staff); } }
        
        [TestMethod]
        public void GetCurrentTest()
        {
            StaffMemberResponse result = Service.Current();
            Assert.IsTrue(result.IsValid());
            Assert.IsTrue(result.HasStaff);
            Assert.IsTrue(result.Staff.HasStaffId);
            Assert.AreNotEqual(0, result.Staff.StaffId);
        }

        [TestMethod]
        public void GetByStaffIdTest()
        {
            StaffMemberResponse result = Service.Current();
            Assert.IsTrue(result.IsValid());
            Assert.IsTrue(result.HasStaff);
            Assert.IsTrue(result.Staff.HasStaffId);

            StaffMemberResponse result2 = Service.Get(
                new StaffMemberIdentity
                    {
                        StaffId = result.Staff.StaffId,
                    }
                );

            Assert.IsTrue(result.Staff.StaffId.Equals(result2.Staff.StaffId));
        }

        [TestMethod]
        public void GetListTest()
        {
            StaffMember me = Service.Current().Staff;
            StaffMembersResponse list = Service.List(new StaffMembersRequest());

            Assert.AreNotEqual(0, list.StaffMembers.MemberList.Count);
            Assert.AreEqual(1, list.StaffMembers.MemberList.Count(s => s.StaffId == me.StaffId));
        }
    }
}
