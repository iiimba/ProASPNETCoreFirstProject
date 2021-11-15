﻿using GOFPatternExamples.AbstractFactory;
using GOFPatternExamples.AbstractFactory.Factory;
using GOFPatternExamples.Adapter;
using GOFPatternExamples.Bridge;
using GOFPatternExamples.Builder;
using GOFPatternExamples.Composite;
using GOFPatternExamples.Decorator;
using GOFPatternExamples.FactoryMethod;
using GOFPatternExamples.Prototype;
using GOFPatternExamples.Singleton;
using System;

namespace GOFPatternExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractFactoryExample();

            BuilderExample();

            FactoryMethodExample();

            PrototypeExample();

            SingletonExample();

            AdaptorExample();

            BridgeExample();

            CompositeExample();

            DecoratorExample();

            Console.ReadKey();
        }

        /// <summary>
        /// You can also use the product family instead of using the Run method
        /// </summary>
        static void AbstractFactoryExample()
        {
            var client = new AbstractFactoryClient(new VictorianFactory());
            client.Run();

            client = new AbstractFactoryClient(new ModernFactory());
            client.Run();
        }

        static void BuilderExample()
        {
            var director = new Director();

            var renoBuilder = new RenoBuilder();
            director.MakeSportCar(renoBuilder);
            var renoCar = renoBuilder.GetResult();

            var opelBuilder = new OpelBuilder();
            director.MakeSportCar(opelBuilder);
            var opelCar = opelBuilder.GetResult();
        }

        static void FactoryMethodExample()
        {
            var roadLogistics = new RoadLogistics();
            roadLogistics.PlanDelivery();
            var truck = roadLogistics.FactoryMethod();

            var seaLogistics = new SeaLogistics();
            seaLogistics.PlanDelivery();
            var ship = seaLogistics.FactoryMethod();
        }

        static void PrototypeExample()
        {
            var developer = new Developer("Vlad", 1000, "C#");
            var developerClone = developer.Clone() as Developer;

            Console.WriteLine(developerClone);
        }

        static void SingletonExample()
        {
            var singleton1 = Singleton.Singleton.GetSingleton();
            var singleton2 = Singleton.Singleton.GetSingleton();

            Console.WriteLine(singleton1.GetHashCode());
            Console.WriteLine(singleton2.GetHashCode());

            var singletonForMultithreading1 = SingletonForMultithreading.GetSingleton();
            var singletonForMultithreading2 = SingletonForMultithreading.GetSingleton();

            Console.WriteLine(singletonForMultithreading1.GetHashCode());
            Console.WriteLine(singletonForMultithreading2.GetHashCode());

            var singletonForMultithreadingWithoutLock1 = SingletonForMultithreadingWithoutLock.GetSingleton();
            var singletonForMultithreadingWithoutLock2 = SingletonForMultithreadingWithoutLock.GetSingleton();

            Console.WriteLine(singletonForMultithreadingWithoutLock1.GetHashCode());
            Console.WriteLine(singletonForMultithreadingWithoutLock2.GetHashCode());
        }

        static void AdaptorExample()
        {
            ITarget target1 = new ClassAdapter();
            target1.Request();

            Target target2 = new ObjectAdapter();
            target2.Request();
        }

        static void BridgeExample()
        {
            var message1 = new SystemMessage(new MailSender()) { Subject = "Subject1", Body = "Body1" };
            message1.Send();

            var message2 = new UserMessage(new WebRequestSender()) { Subject = "Subject2", Body = "Body2", Comment = "Comment2" };
            message2.Send();
        }

        static void CompositeExample()
        {
            var mainBox = new Box("MainBox", 10);
            var smallerBox = new Box("SmallerBox", 5);
            var smallestBox = new Box("SmallestBox", 2);

            var phone = new Product("Phone", 1299);
            var headset = new Product("Headset", 359);
            var charger = new Product("Charger", 15.99m);
            var memoryCard = new Product("MemoryCard", 29);

            mainBox.Add(phone);
            mainBox.Add(smallerBox);

            smallerBox.Add(headset);
            smallerBox.Add(charger);
            smallerBox.Add(smallestBox);

            smallestBox.Add(memoryCard);

            mainBox.Operation();

            Console.WriteLine($"Full price: {mainBox.GetPrice()}");
        }

        static void DecoratorExample()
        {
            var emailSender = new EmailSender();
            emailSender.Send("12345@mail.com", "Hello world!");

            var skypeSender = new SkypeSender(emailSender){ NickName = "Jack" };
            skypeSender.Send("12345@mail.com", "Hello world!");

            var facebookSender = new FacebookSender(skypeSender);
            facebookSender.Send("12345@mail.com", "Hello world!");
        }
    }
}
