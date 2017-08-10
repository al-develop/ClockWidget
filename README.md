# ClockWidget - What is it and how do I use it?
This simple tool is a small Widget styled WPF Applciation.
It's basically a timre, which tells you how much time you hve left to a specific time.
It's easy to use - you control the Application over a context menu, there you can specify, what time is the final time.
The timer is self updating, therefore you don't need to click "save" or anything - just put in the time and everythign is done.
You also can specifiy a note, which will appear in a Tooltip, so you won't forget, what appointment you planned when the timer is up (for example,
you might be a wokraholic and need a reminder to call it a day and go home)
Because it will not appear in your taskbar, there's a Tray icon, in case you have lost the widget. the tray Icon uspports the same context menu commands as the UI.

![preview](https://github.com/al-develop/ClockWidget/blob/master/clockwidget.png)

# For Developers
If you want to extend the possiblities of this widget (For example, a sound notification would be cool. Or maybe something like multiple timers, for diiferent appointments).
If you plan to extend the context menu, keep in mind, to extend it on both the UI and the Try Icon. 
I used the DevExpress.Mvvm Library from NuGet and a fw other Libraries for this Application (you can find a complete list below).
Moreover, the Applcaiton is pretty small, so there isn't much magic behind it at all. It's prettyx straightforward, just a bucnh of classes and a few lines of code.
