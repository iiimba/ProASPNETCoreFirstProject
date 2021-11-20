using GOFPatternExamples.AbstractFactory;
using GOFPatternExamples.AbstractFactory.Factory;
using GOFPatternExamples.Adapter;
using GOFPatternExamples.Bridge;
using GOFPatternExamples.Builder;
using GOFPatternExamples.ChainOfResponsibility;
using GOFPatternExamples.Command;
using GOFPatternExamples.Composite;
using GOFPatternExamples.Decorator;
using GOFPatternExamples.FactoryMethod;
using GOFPatternExamples.Flyweight;
using GOFPatternExamples.Interpreter;
using GOFPatternExamples.Iterator;
using GOFPatternExamples.Mediator;
using GOFPatternExamples.Memento;
using GOFPatternExamples.Prototype;
using GOFPatternExamples.Singleton;
using GOFPatternExamples.State.RealExample;
using GOFPatternExamples.Strategy;
using GOFPatternExamples.TemplateMethod;
using GOFPatternExamples.Visitor;
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

            FacadeExample();

            FlyweightExample();

            ProxyExample();

            ChainOfResponsibility();

            CommandExample();

            InterpreterExample();

            IteratorExample();

            MediatorExample();

            MementoExample();

            ObserverPullModelExample();
            ObserverPushModelExample();

            StateExample();
            StateRealAwkwardExample();

            StrategyExample();

            TemplateMethodExample();

            VisitorExample();

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

        static void FacadeExample()
        {
            var facade = new Facade.Facade();
            facade.MethodA();
            facade.MethodB();
        }

        static void FlyweightExample()
        {
            var flyweightFactory = new FlyweightFactory();
            var flyweight1 = flyweightFactory.GetFlyweight("Test", 11);
            var flyweight2 = flyweightFactory.GetFlyweight("Test", 12);

            Console.WriteLine(flyweight1 == flyweight2);

            flyweight1.Operation(5);
            flyweight2.Operation(6);

            var flyweight3 = new UnsharedFlyweight();
            flyweight3.Operation(7);
        }

        static void ProxyExample()
        {
            var proxy = new Proxy.Proxy();
            proxy.Request();
        }

        static void ChainOfResponsibility()
        {
            var handler1 = new ExceptionHandler1();
            var handler2 = new ExceptionHandler2();
            var handler3 = new ExceptionHandler3();

            handler1.SetSuccessor(handler2);
            handler2.SetSuccessor(handler3);

            handler1.Handle(1);
            handler1.Handle(2);
            handler1.Handle(3);
        }

        static void CommandExample()
        {
            var calculator = new Calculator();
            Console.WriteLine(calculator.Add(50));
            Console.WriteLine(calculator.Sub(4));
            Console.WriteLine(calculator.Mul(10));
            Console.WriteLine(calculator.Redo());
            Console.WriteLine(calculator.Undo());
            Console.WriteLine(calculator.Undo());
            Console.WriteLine(calculator.Undo());
            Console.WriteLine(calculator.Redo());
        }

        static void InterpreterExample()
        {
            var context = new Context();
            context.Source = "aaa";
            context.Vocabulary = 'a';

            var expression = new NonTerminalExpression();
            expression.Interpret(context);

            Console.WriteLine(context.Result);

            context.Source = "aab";
            context.Vocabulary = 'a';
            context.Position = 0;

            expression.Interpret(context);

            Console.WriteLine(context.Result);
        }

        static void IteratorExample()
        {
            var aggregate = new ConcreteAggregate();
            aggregate.Add(3);
            aggregate.Add(5);
            aggregate.Add(2);

            var iterator = aggregate.CreateIterator();

            for (object current = iterator.First(); !iterator.IsDone(); current = iterator.Next())
            {
                Console.WriteLine(current);
            }
        }

        static void MediatorExample()
        {
            var mediator = new ConcreteMediator();
            var colleague1 = new Colleague1(mediator);
            var colleague2 = new Colleague2(mediator);
            mediator.Colleague1 = colleague1;
            mediator.Colleague2 = colleague2;

            colleague1.Send($"Hello {nameof(Colleague2)}!");
            colleague2.Send($"Hello {nameof(Colleague1)}!");
        }

        static void MementoExample()
        {
            var originator = new Originator();
            originator.State = "Initial state";

            var caretaker = new Caretaker();
            caretaker.Memento = originator.CreateMemento();

            originator.State = "New state";

            originator.SetMemento(caretaker.Memento);

            Console.WriteLine(originator.State);
        }

        static void ObserverPullModelExample()
        {
            var publisher = new Observer.Pull.ConcreteSubject();
            publisher.State = "State1";

            publisher.Attach(new Observer.Pull.ConcreteObserver(publisher));
            publisher.Attach(new Observer.Pull.ConcreteObserver(publisher));

            var observer3 = new Observer.Pull.ConcreteObserver(publisher);
            publisher.Attach(observer3);

            publisher.Notify();

            publisher.Detach(observer3);

            publisher.State = "State2";

            publisher.Notify();
        }

        static void ObserverPushModelExample()
        {
            var publisher = new Observer.Push.ConcreteSubject();
            publisher.State = "State1";

            publisher.Attach(new Observer.Push.ConcreteObserver());
            publisher.Attach(new Observer.Push.ConcreteObserver());

            var observer3 = new Observer.Push.ConcreteObserver();
            publisher.Attach(observer3);

            publisher.Notify();

            publisher.Detach(observer3);

            publisher.State = "State2";

            publisher.Notify();
        }

        static void StateExample()
        {
            var context = new State.Context(new State.ConcreteState1());
            context.Request();
            context.Request();
            context.Request();
        }

        static void StateRealAwkwardExample()
        {
            var account = new Account();

            Console.WriteLine(account.InterestOnLoan);

            account.AddBalance(10_000);
            account.RequestToChangeCardState();

            Console.WriteLine(account.InterestOnLoan);

            account.AddBalance(1_500_000);
            account.RequestToChangeCardState();

            Console.WriteLine(account.InterestOnLoan);
        }

        static void StrategyExample()
        {
            var context = new SomeArray();
            context.Sort();
            context.ShowItems();

            context.Strategy = new ShellSort();

            context.Sort();
            context.ShowItems();
        }

        static void TemplateMethodExample()
        {
            var instance = new DerivedClass();
            instance.TemplateMethod();
        }

        static void VisitorExample()
        {
            var structure = new ObjectStructure();

            structure.Add(new ConcreteElement1());
            structure.Add(new ConcreteElement2());
            structure.Add(new ConcreteElement1());

            structure.Accept(new ConcreteVisitor());
        }
    }
}
