命令（Command）模式

命令（Command）模式属于对象的行为模式【GOF95】。
命令模式又称为行动（Action）模式或交易（Transaction）模式。
命令模式把一个请求或者操作封装到一个对象中。
命令模式允许系统使用不同的请求把客户端参数化，对请求排队或者记录请求日志，可以提供命令的撤销和恢复功能。


命令模式是对命令的封装。命令模式把发出命令的责任和执行命令的责任分割开，委派给不同的对象。


每一个命令都是一个操作：
请求的一方发出请求要求执行一个操作；接收的一方收到请求，并执行操作。
命令模式允许请求的一方和接收的一方独立开来，使得请求的一方不必知道接收请求的一方的接口，
更不必知道请求是怎么被接收，以及操作是否被执行、何时被执行，以及是怎么被执行的。


命令模式涉及到五个角色，它们分别是：

客户（Client）角色：
    创建了一个具体命令(ConcreteCommand)对象并确定其接收者。

命令（Command）角色：
    声明了一个给所有具体命令类的抽象接口。这是一个抽象角色。

具体命令（ConcreteCommand）角色：
    定义一个接受者和行为之间的弱耦合；实现Execute()方法，负责调用接收考的相应操作。Execute()方法通常叫做执行方法。

请求者（Invoker）角色：
    负责调用命令对象执行请求，相关的方法叫做行动方法。

接收者（Receiver）角色：
    负责具体实施和执行一个请求。任何一个类都可以成为接收者，实施和执行请求的方法叫做行动方法。 



	
	
在什么情况下应当使用命令模式

在下面的情况下应当考虑使用命令模式：

1、使用命令模式作为"CallBack"在面向对象系统中的替代。
    "CallBack"讲的便是先将一个函数登记上，然后在以后调用此函数。

2、需要在不同的时间指定请求、将请求排队。
    一个命令对象和原先的请求发出者可以有不同的生命期。
	换言之，原先的请求发出者可能已经不在了，而命令对象本身仍然是活动的。
	这时命令的接收者可以是在本地，也可以在网络的另外一个地址。
	命令对象可以在串形化之后传送到另外一台机器上去。

3、系统需要支持命令的撤消(undo)。
    命令对象可以把状态存储起来，等到客户端需要撤销命令所产生的效果时，可以调用undo()方法，把命令所产生的效果撤销掉。
	命令对象还可以提供redo()方法，以供客户端在需要时，再重新实施命令效果。

4、如果一个系统要将系统中所有的数据更新到日志里，以便在系统崩溃时，可以根据日志里读回所有的数据更新命令，
    重新调用Execute()方法一条一条执行这些命令，从而恢复系统在崩溃前所做的数据更新。

5、一个系统需要支持交易(Transaction)。
    一个交易结构封装了一组数据更新命令。使用命令模式来实现交易结构可以使系统增加新的交易类型。


	
	
	
	
使用命令模式的优点和缺点

命令允许请求的一方和接收请求的一方能够独立演化，从而且有以下的优点：

1、命令模式使新的命令很容易地被加入到系统里。
2、允许接收请求的一方决定是否要否决（Veto）请求。
3、能较容易地设计一个命令队列。
4、可以容易地实现对请求的Undo和Redo。
5、在需要的情况下，可以较容易地将命令记入日志。
6、命令模式把请求一个操作的对象与知道怎么执行一个操作的对象分割开。
7、命令类与其他任何别的类一样，可以修改和推广。
8、你可以把命令对象聚合在一起，合成为合成命令。比如宏命令便是合成命令的例子。合成命令是合成模式的应用。
9、由于加进新的具体命令类不影响类，因此增加新的具体命令类很容易。


命令模式的缺点如下：

使用命令模式会导致某些系统有过多的具体命令类。
    某些系统可能需要几十个，几百个甚至几千个具体命令类，这会使命令模式在这样的系统里变得不实际。


	