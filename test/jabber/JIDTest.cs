/* --------------------------------------------------------------------------
 * Copyrights
 * 
 * Portions created by or assigned to Cursive Systems, Inc. are 
 * Copyright (c) 2002-2004 Cursive Systems, Inc.  All Rights Reserved.  Contact
 * information for Cursive Systems, Inc. is available at
 * http://www.cursive.net/.
 *
 * License
 * 
 * Jabber-Net can be used under either JOSL or the GPL.  
 * See LICENSE.txt for details.
 * --------------------------------------------------------------------------*/
using System;

using bedrock.util;
using NUnit.Framework;
using je = jabber.JIDFormatException;
using jabber;

namespace test.jabber
{
    /// <summary>
    /// Summary description for JIDTest.
    /// </summary>
    [RCS(@"$Header$")]
    [TestFixture]
    public class JIDTest
    {
        public void Test_Create()
        {
            JID j = new JID("foo", "jabber.com", "there");
            Assertion.AssertEquals("foo@jabber.com/there", j.ToString());
            j = new JID(null, "jabber.com", null);
            Assertion.AssertEquals("jabber.com", j.ToString());
            j = new JID("foo", "jabber.com", null);
            Assertion.AssertEquals("foo@jabber.com", j.ToString());
            j = new JID(null, "jabber.com", "there");
            Assertion.AssertEquals("jabber.com/there", j.ToString());
        }
        public void Test_Parse_1()
        {
            JID j = new JID("foo");
            Assertion.AssertEquals(null, j.User);
            Assertion.AssertEquals("foo", j.Server);
            Assertion.AssertEquals(null, j.Resource);
        }
        public void Test_Parse_2()
        {
            JID j = new JID("foo/bar");
            Assertion.AssertEquals(null, j.User);
            Assertion.AssertEquals("foo", j.Server);
            Assertion.AssertEquals("bar", j.Resource);
        }
        public void Test_Parse_3()
        {
            JID j = new JID("boo@foo");
            Assertion.AssertEquals("boo", j.User);
            Assertion.AssertEquals("foo", j.Server);
            Assertion.AssertEquals(null, j.Resource);
        }
        public void Test_Parse_4()
        {
            JID j = new JID("boo@foo/bar");
            Assertion.AssertEquals("boo", j.User);
            Assertion.AssertEquals("foo", j.Server);
            Assertion.AssertEquals("bar", j.Resource);
        }
        public void Test_Parse_5()
        {
            JID j = new JID("foo/bar@baz");
            Assertion.AssertEquals(null, j.User);
            Assertion.AssertEquals("foo", j.Server);
            Assertion.AssertEquals("bar@baz", j.Resource);
        }
        public void Test_Parse_6()
        {
            JID j = new JID("boo@foo/bar@baz");
            Assertion.AssertEquals("boo", j.User);
            Assertion.AssertEquals("foo", j.Server);
            Assertion.AssertEquals("bar@baz", j.Resource);
        }
        public void Test_Parse_7()
        {
            JID j = new JID("boo@foo/bar/baz");
            Assertion.AssertEquals("boo", j.User);
            Assertion.AssertEquals("foo", j.Server);
            Assertion.AssertEquals("bar/baz", j.Resource);
        }
        public void Test_Parse_8()
        {
            JID j = new JID("boo/foo@bar@baz");
            Assertion.AssertEquals(null, j.User);
            Assertion.AssertEquals("boo", j.Server);
            Assertion.AssertEquals("foo@bar@baz", j.Resource);
        }
        public void Test_Parse_9()
        {
            JID j = new JID("boo/foo@bar");
            Assertion.AssertEquals(null, j.User);
            Assertion.AssertEquals("boo", j.Server);
            Assertion.AssertEquals("foo@bar", j.Resource);
        }
        public void Test_Parse_10()
        {
            JID j = new JID("boo/foo/bar");
            Assertion.AssertEquals(null, j.User);
            Assertion.AssertEquals("boo", j.Server);
            Assertion.AssertEquals("foo/bar", j.Resource);
        }
        public void Test_Parse_11()
        {
            JID j = new JID("boo//foo");
            Assertion.AssertEquals(null, j.User);
            Assertion.AssertEquals("boo", j.Server);
            Assertion.AssertEquals("/foo", j.Resource);
        }
        public void Test_EmptyResource()
        {
            try
            {
                JID j = new JID("boo/");
                string u = j.User;
                Assertion.Assert(false);
            }
            catch (JIDFormatException)
            {
                Assertion.Assert(true);
            }
            catch (Exception)
            {
                Assertion.Assert(false);
            }
        }
        public void Test_EmptyResourceUser()
        {
            try
            {
                JID j = new JID("boo@foo/");
                string u = j.User;
                Assertion.Assert(false);
            }
            catch (JIDFormatException)
            {
                Assertion.Assert(true);
            }
            catch (Exception)
            {
                Assertion.Assert(false);
            }
        }

        public void Test_NoHost()
        {
            try
            {
                JID j = new JID("foo@");
                string u = j.User;
                Assertion.Assert(false);
            }
            catch (JIDFormatException)
            {
                Assertion.Assert(true);
            }
            catch (Exception)
            {
                Assertion.Assert(false);
            }
        }
        public void Test_AtAt()
        {
            try
            {
                JID j = new JID("boo@@foo");
                string u = j.User;
                Assertion.Assert(false);
            }
            catch (JIDFormatException)
            {
                Assertion.Assert(true);
            }
            catch (Exception)
            {
                Assertion.Assert(false);
            }
        }
        public void Test_TwoAt()
        {
            try
            {
                JID j = new JID("boo@foo@bar");
                string u = j.User;
                Assertion.Assert(false);
            }
            catch (JIDFormatException)
            {
                Assertion.Assert(true);
            }
            catch (Exception)
            {
                Assertion.Assert(false);
            }
        }
        public void Test_Compare_Equal()
        {
            JID j = new JID("foo@bar/baz");
            Assertion.AssertEquals(0, j.CompareTo(j));
            Assertion.AssertEquals(0, j.CompareTo(new JID("foo@bar/baz")));
            j = new JID("foo@bar");
            Assertion.AssertEquals(0, j.CompareTo(j));
            Assertion.AssertEquals(0, j.CompareTo(new JID("foo@bar")));
            Assertion.Assert(j == new JID("foo@bar"));
            Assertion.Assert(j == new JID("foo@BAR"));
            Assertion.Assert(j == new JID("FOO@BAR"));
            Assertion.Assert(j == new JID("FOO@bar"));
            Assertion.AssertEquals(new JID("FOO@bar").GetHashCode(), j.GetHashCode());
            j = new JID("bar");
            Assertion.AssertEquals(0, j.CompareTo(j));
            Assertion.AssertEquals(0, j.CompareTo(new JID("bar")));
            j = new JID("foo/bar");
            Assertion.AssertEquals(0, j.CompareTo(j));
            Assertion.AssertEquals(0, j.CompareTo(new JID("foo/bar")));
            Assertion.AssertEquals(true, j >= new JID("foo/bar"));
            Assertion.AssertEquals(true, j <= new JID("foo/bar"));
        }
        public void Test_Compare_Less()
        {
            JID j = new JID("foo@bar/baz");
            Assertion.AssertEquals(-1, j.CompareTo(new JID("foo@bas/baz")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("fop@bar/baz")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("foo@bar/bazz")));
            j = new JID("foo@bar");
            Assertion.AssertEquals(-1, j.CompareTo(new JID("foo@bas/baz")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("foo@bas")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("fop@bar/baz")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("fop@bar")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("foo@bar/baz")));
            j = new JID("bar");
            Assertion.AssertEquals(-1, j.CompareTo(new JID("foo@bar/baz")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("foo@bar")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("bas")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("bas/baz")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("bar/baz")));
            Assertion.AssertEquals(true, j < new JID("foo@bar/baz"));
            Assertion.AssertEquals(true, j <= new JID("foo@bar/baz"));
        }
        public void Test_Compare_Greater()
        {
            JID j = new JID("foo@bar/baz");
            Assertion.AssertEquals(1, j.CompareTo(new JID("foo@bap/baz")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("fon@bar/baz")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("foo@bar/bay")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("foo@bar")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("bar")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("bar/baz")));
            j = new JID("foo@bar");
            Assertion.AssertEquals(1, j.CompareTo(new JID("foo@bap/baz")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("fon@bar/baz")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("bar")));
            Assertion.AssertEquals(1, j.CompareTo(new JID("bar/baz")));
            Assertion.AssertEquals(true, j > new JID("foo@bap/baz"));
            Assertion.AssertEquals(true, j >= new JID("foo@bap/baz"));
            // /me runs out of interest.
        }
        public void Test_BadCompare()
        {
            try
            {
                JID j = new JID("foo@boo/bar");
                j.CompareTo("foo@boo/bar");
                Assertion.Assert(false);
            }
            catch (ArgumentException)
            {
                Assertion.Assert(true);
            }
            catch (Exception)
            {
                Assertion.Assert(false);
            }
        }
        public void Test_Insensitive()
        {
            JID j = new JID("foo@boo/bar");
            Assertion.AssertEquals(0, j.CompareTo(new JID("foo@BOO/bar")));
            Assertion.AssertEquals(0, j.CompareTo(new JID("FOO@boo/bar")));
            Assertion.AssertEquals(0, j.CompareTo(new JID("FOO@BOO/bar")));
            Assertion.AssertEquals(-1, j.CompareTo(new JID("FOO@BOO/BAR")));
        }

        public void Test_Config()
        {
            JID j = new JID("config@-internal");
            Assertion.AssertEquals("config", j.User);
            Assertion.AssertEquals("-internal", j.Server);
            Assertion.AssertEquals(null, j.Resource);
        }

        public void Test_Numeric()
        {
            JID j = new JID("support", "conference.192.168.32.109", "bob");
            Assertion.AssertEquals("conference.192.168.32.109", j.Server);
        }
    }
}