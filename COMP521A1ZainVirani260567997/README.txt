Hello and welcome to my assignment.

Most of the requirements are satisfied, here is a list of what isn't (as far as I know)
1. Boulders don't traverse the maze
They are still destructible, and still cause a LOSE condition if they touch the player
And they still spawn every 5s at the maze entrance

One issue I have is that sometimes unity takes very long to generate the maze
If it does occur and you're sick of waiting just restart unity
I am pretty confident this happens because the maze generator traps itself somewhere along the line
But most of the time it generates perfectly well

Also note that in my code you will find that the application does quit 5s after WIN or LOSE
It just doesn't do that in the Editor because Unity is silly :/

Finally, all scripts I wrote myself are in the "Scripts" folder in the main directory
Anything I didn't write myself is either in "Standard Assets" or "Free Assets"
ShotCount.cs is not relevant to the project (it's disabled), it's just something I did for fun.

Thanks for reading :)