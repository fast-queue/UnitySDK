# Fast-Queue Unity-SDK #

This SDK are made for developers who intend to use the Fast-Queue framework to create a lobby for multiplayer games or any other application that needs multi users on a lobby. 

## Requesting a Key ##
There is a need to be using a Key (as a oAuth authentication) to be able to use the API and, because this is a under development project and i'm yet creating a website to be responsible to the user's inscriptions to the project, i'll ask you all to send me a request via this [Form](https://goo.gl/forms/16QssDzI9JzE3W3b2) while i'm not able to create the website.

Thanks

## Installation ##

Download the package from [Release](https://github.com/fast-queue/UnitySDK/releases) page, import on UNITY and use.


## Documentation ##

Yet to be developed, but you can base on NodeJS api documentation. 

See the [Documentation](https://fast-queue.github.io/NodeJS-API/) page for more help. 
Functions at index module.

## Usage ## 

### Basic ##
There is [this](https://github.com/fast-queue/UnitySDK/blob/master/Assets/FAST-QUEUE-SDK/Model/BaseBody.cs) class that are the base for all request that includes something in the system, so, all models that you develop, (your queue class, player class) should extends from this class to be able to communicate with the server.
All attributes from the class that need to be sent to the server have to be public, because the JSON api from UNITY only parse if the attribute from class are public.

Ex:
```C#
class MyPlayer: FQ.BaseBody {

    public string name;
    public int ranking;
    
    }
```

And it is needed for the Queue class too.

Here is a more complete example: 

```C#
public class Queue : FQ.BaseBody{
    public int minRanking;
    public int maxPlayers;

    // Initialize the queue
    public Queue(int minRanking, int maxPlayers)
    {
        this.minRanking = minRanking;
        this.maxPlayers = maxPlayers;
        // add the queue to the server when it's initilized
        this._id = Manager.Instance.api.addQueue<Queue>(this)._id; // get the id from the server
    }

    // Delete the instance of the queue on server-side when it's deleted
    ~Queue
        ()
    {
        Manager.Instance.api.deleteQueue<Queue>(this);
    }

    // Simple player quantity controll.
    public bool addPlayer(Player player)
    {
        if (Manager.Instance.api.getPlayers<Queue, Player>(this).Length >= this.maxPlayers)
        {
            return false;
        }
        Manager.Instance.api.addPlayer<Queue, Player>(this, player);
        return true;
    }

}
```


### Starting the API ###
For the access Key visit this [LINK](https://goo.gl/forms/16QssDzI9JzE3W3b2)

A nice pattern is to use this in a singleton to manage the instance.
```C#
using FQ;

string url = "http://tcc-andre.ddns.net";
string key = "MyKeyGivenFromForm";

RestApi api = new RestApi(url, key));
```

### Adding a Queue ##

Returns the object from MyQueue that the server created.

```c#
// Add a queue:
MyQueue q = api.addQueue<MyQueue>(queue);

```

### Add a player to Queue ###
It returns a new Player of the type player.
```c#

MyPlayer p = api.addPlayer<MyQueue, MyPlayer>(queue, player);
```

### Get all Queues ###
It gets all queues that are active for the Key inserted.

```c#
MyQueue[] q = api.getAllQueue<MyQueue>();

```

### Get all players from a queue ###
Return an player's array without all the info, only with the id.

```C#
MyPlayerClass[] p = api.getPlayers<MyQueue, MyPlayer>(queue);
```

### Get player info ###
You get all the player infos
```C#

MyPlayer p = api.getAPlayer<MyQueue, MyPlayer>(queue, player)

```

### Delete a queue from server ###

It return the queue with only the Queue id inherit, so, if you missdelete the queue, it won't have a copy anywhere. 

```C#
MyQueue q = api.deleteQueue<MyQueue>(queue);

```

### Delete a player from a queue
It return the queue with only the player id inherit.
```C#
MyPlayer p = api.deletePlayer<MyQueue, MyPlayer>(queue, player);

```

## Thanks ##

Thanks all that helped me:
* [ViniGodoy](https://github.com/vinigodoy)
* [Alisson](https://github.com/alissonads)
* Thais Weiller
* Déborah Carvalho
