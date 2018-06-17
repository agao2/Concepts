using System;

namespace Delegates_Events
{
    class Program
    {
        /*
        Events are a way for objects to communicate between each other
        Helps make your application more loosely coupled
        It also can allow you to extend your application without having to recompile 
        the entire application

        Delegates are essentially a pointer to a method/function
         */
        static void Main(string[] args)
        {
            Video video = new Video() { Title = "10 Best Cities to Live In" };
            Channel channel = new Channel() { Title = "My Channel" }; //publisher
            User user = new User() { Name = "Alex" }; //subscriber

            /* Look at how the event handler(method) is just added to an event using += 
            note: how we don't use () to invoke the method, we really only want the reference to that method
            also note that we use += like we're just adding a pointer to a list of pointers*/
            channel.VideoPublished += user.OnVideoPublished;
            channel.VideoPublished += user.OnNewComment;

            /*
            Note that you can create more classes and more methods and add it onto the event without
            needing the class that has the event to be recompiled! 
             */

            channel.Publish(video);
        }
    }

    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }


    public class Channel
    {
        public string Title { get; set; }

        /*note that the two parameters are optional and are purely a convention sort of things
        first param = object as source of event and  EventArgs args = any additional data 
        Note: instead of having EventArgs as the second parameter, we can create a new class
        that derives from the EventArgs class*/
        public delegate void VideoPublishedEventHandler(object source, VideoEventArgs args);

        /* Here we define an event based on the delegate. We need a method that is responsible for 
        raising the event.
        Note the syntax: public event {delegate name} {variable name}*/
        public event VideoPublishedEventHandler VideoPublished;


        // Note that there is an EventHandler type or EventHandler<TEventArgs> type so we don't have to create a delegate separately 
        public event EventHandler<VideoEventArgs> VideoPublished2; // essnetially the same as the above 2 lines of code !
                                                                   // this means we can write less code, but the above 2 lines is how it works still

        /*  Naming convention is On{event name} 
        We use a protected virtual method when raising events where
        the event is non-static and defining class is inheritable to 
        allow derived classes to handle a base class event*/
        protected virtual void OnVideoPublished(Video video)
        {
            /* Event is like a list of methods 
            Below we look at that list, if its not empty someone has subscribed
            Then we call all the methods in the event*/
            if (VideoPublished != null)
                VideoPublished(this, new VideoEventArgs(){Video = video});
        }

        public void Publish(Video video)
        {
            Console.WriteLine(video.Title + " has been published!");
            OnVideoPublished(video);
        }
    }

    public class User
    {
        public string Name { get; set; }

        public void OnVideoPublished(object source, VideoEventArgs e)
        {
            Console.WriteLine(Name + " ,Watch this new video, " + e.Video.Title + " that has just been published");
        }

        public void OnNewComment(object source ,VideoEventArgs e)
        {
            Console.WriteLine(Name + " ,Check out new comments in a video you've viewed. Video:" + e.Video.Title );
        }
    }

    public class Video
    {
        public string Title { get; set; }
    }

}
