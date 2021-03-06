﻿//******************************************************************************
//
// Copyright (c) 2016 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace W3CWebDriver
{
    [TestClass]
    public class TouchScroll : AlarmClockBase
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestMethod]
        public void Scroll()
        {
            var alarmPivotItem = session.FindElementByAccessibilityId("AlarmPivotItem");
            var stopwatchPivotItem = session.FindElementByAccessibilityId("StopwatchPivotItem");
            Assert.IsNotNull(alarmPivotItem);
            Assert.IsNotNull(stopwatchPivotItem);
            Assert.IsTrue(alarmPivotItem.Selected);
            Assert.IsFalse(stopwatchPivotItem.Selected);

            // Perform scroll right touch action to switch from Alarm tab to Stopwatch tab
            touchScreen.Scroll(100, 0);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            Assert.IsFalse(alarmPivotItem.Selected);
            Assert.IsTrue(stopwatchPivotItem.Selected);

            // Perform scroll right touch action to scroll back from Stopwatch tab to Alarm tab
            touchScreen.Scroll(-100, 0);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            Assert.IsTrue(alarmPivotItem.Selected);
            Assert.IsFalse(stopwatchPivotItem.Selected);
        }

        [TestMethod]
        public void ScrollOnElementHorizontal()
        {
            var homePagePivot = session.FindElementByAccessibilityId("HomePagePivot");
            var alarmPivotItem = session.FindElementByAccessibilityId("AlarmPivotItem");
            var worldClockPivotItem = session.FindElementByAccessibilityId("WorldClockPivotItem");
            Assert.IsNotNull(homePagePivot);
            Assert.IsNotNull(alarmPivotItem);
            Assert.IsNotNull(worldClockPivotItem);
            Assert.IsTrue(alarmPivotItem.Selected);
            Assert.IsFalse(worldClockPivotItem.Selected);

            // Perform scroll left touch action to switch from Alarm tab to WorldClock tab
            touchScreen.Scroll(homePagePivot.Coordinates, - 100, 0);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            Assert.IsFalse(alarmPivotItem.Selected);
            Assert.IsTrue(worldClockPivotItem.Selected);

            // Perform scroll right touch action to scroll back from WorldClock tab to Alarm tab
            touchScreen.Scroll(homePagePivot.Coordinates, 100, 0);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            Assert.IsTrue(alarmPivotItem.Selected);
            Assert.IsFalse(worldClockPivotItem.Selected);
        }

        [TestMethod]
        public void ScrollOnElementVertical()
        {
            // Navigate to add alarm page
            session.FindElementByAccessibilityId("AddAlarmButton").Click();
            session.FindElementByAccessibilityId("AlarmTimePicker").Click();
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second

            var minuteSelector = session.FindElementByAccessibilityId("MinuteLoopingSelector");
            var minute00 = session.FindElementByName("00");
            var minute05 = session.FindElementByName("05");
            Assert.IsNotNull(minuteSelector);
            Assert.IsNotNull(minute00);
            Assert.IsNotNull(minute05);
            Assert.IsTrue(minute00.Displayed);
            Assert.IsFalse(minute05.Displayed);

            // Perform scroll down touch action to scroll the minute showing 05 minutes that was hidden
            touchScreen.Scroll(minuteSelector.Coordinates, 0, -55);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            Assert.IsFalse(minute00.Displayed);
            Assert.IsTrue(minute05.Displayed);

            // Perform scroll up touch action to scroll the the minute back showing 00 minutes that was shown
            touchScreen.Scroll(minuteSelector.Coordinates, 0, 55);
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            Assert.IsTrue(minute00.Displayed);
            Assert.IsFalse(minute05.Displayed);

            // Navigate back to the original view
            session.Navigate().Back();
        }
    }
}
