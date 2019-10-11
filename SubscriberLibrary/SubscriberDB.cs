using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SubscriberLibrary
{
    public class SubscriberDB
    {
        public static Boolean Load(ref List<Subscriber> subcribers)
        {
            Subscriber mySubscriber = new Subscriber();
            mySubscriber.email = "alex.matthias@pcc.com";
            subcribers.Add(mySubscriber);
          
            mySubscriber = new Subscriber();
            mySubscriber.email = "abc@abc.com";
            subcribers.Add(mySubscriber);

            mySubscriber = new Subscriber();
            mySubscriber.email = "blah@blah.com";
            subcribers.Add(mySubscriber);

            return true;
        }
    }
}
