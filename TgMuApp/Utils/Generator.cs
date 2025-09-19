using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgMuApp.Utils
{
    public class Generator
    {
        private static List<string> AutoMsgList()
        {
            return new List<string> {

               "Hey, what's up?",
                "Not much, just hanging out. How about you?",
                "Same here, just watching some TV.",
                "Cool.Anything interesting?",
                "Not really, just a sitcom I've seen a million times.",
                "Ha, sounds like me too.",
                "Alright then, what do you want to do?",
                "I don't know, what do you think?",
                "Maybe we could go for a walk?",
                "Sure, that sounds like a good idea.",
                "Great, let's go.",
                "Alright, see you in a few minutes.",
                "Hey, did you find the place okay?",
                "Yeah, it wasn't hard to spot.",
                "That's cool. So, what do you want to do?",
                "I don't know, something fun.",
                "Well, we could check out the amusement park.",
                "That sounds like a lot of fun.",
                "Yeah, let's go!",
                "Okay, sounds great.",
                "Hey, did you enjoy the roller coaster ride?",
                "Absolutely, it was such a rush!",
                "Ha, yeah I know what you mean.",
                "So what else do you want to do?",
                "How about the pirate ship?",
                "Sure, sounds like a good plan.",
                "Alright, let's go check it out.",
                "Hey, what do you think about this spot?",
                "It looks nice, let's relax here for a bit.",
                "Sure, that sounds like a plan.",
                "Hey, did you get a chance to try the food here?",
                "Yeah, it was really delicious.",
                "That's great. So, what do you want to do next?",
                "I don't know, what do you have in mind?",
                "We could ride the Ferris wheel.",
                "Yeah, that would be fun.",
                "Alright, let's head that way.",
                "Hey, did you get a chance to see the fireworks?",
                "Yeah, it was really beautiful.",
                "Yeah, it was pretty impressive.",
                "So, what do you want to do now?",
                "I don't know, what about you?",
                "We could ride the bumper cars.",
                "Sure, that sounds like fun.",
                "Alright, let's go!",
                "Hey, did you enjoy the ride?",
                "Yeah, it was a lot of fun!",
                "That's great. What do you want to do next?",
                "I don't know, what do you think?",
                "We could try the mini golf course.",
                "Sure, that would be fun. ",
                "Okay, let's go check it out.",
                "Hey, did you enjoy the game?",
                "Yeah, it was really fun!",
                "That's great. So, what do you want to do next?",
                "I don't know, what about you?",
                "We could check out the arcade.",
                "Sure, sounds like a plan.",
                "Alright, let's go for it.",
                "Hey, did you have fun playing the games?",
                "Yeah, it was really exciting!",
                "That's great. What do you want to do now?",
                "I don't know, what about you?",
                "We could go bowling.",
                "Sure, that sounds like a lot of fun.",
                "Alright, let's go.",
                "Hey, did you enjoy the game?",
                "Yeah, it was really challenging!",
                "That's great. So, what do you want to do next?",
                "I don't know, what do you think?",
                "We could watch a movie.",
                "Sure, that would be nice.",
                "Alright, let's go find one.",
                "Hey, did you like the movie?",
                "Yeah, it was really good!",
                "That's great. So, what do you want to do now?",
                "I don't know, what about you?",
                "We could get some food.",
                "Sure, sounds good to me.",
                "Alright, let's go find something.",
                "Hey, did you enjoy the meal?",
                "Yeah, it was really delicious.",
                "That's great. So, what do you want to do now?",
                "I don't know, what do you think?",
                "We could go for a walk.",
                "Sure, that sounds like a nice idea.",
                "Alright, let's go.",
                "Hey, did you enjoy the stroll?",
                "Yeah, it was really peaceful.",
                "That's great. So, what do you want to do now?",
                "I don't know, what about you?",
                "We could have a picnic.",
                "Sure, that would be nice.",
                "Alright, let's prepare one.",
                "Hey, did you enjoy the picnic?",
                "Yeah, it was really relaxing.",
                "That's great. So, what do you want to do now?",
                "I don't know, what do you think?",
                "We could go for a swim.",
                "Sure, that sounds like fun.",


            };
        }

        public static List<string> RandomMsgList()
        {
            var random = new Random().Next(0, 10);
            var list = AutoMsgList();
            return list.Skip(random).ToList();
        }

        public static List<string> TopMsgList()
        {

            var random = new Random().Next(0, 20);
            var list = AutoMsgList();
            return list.Skip(random).Take(30).ToList();
        }



    }
}
