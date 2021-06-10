# NonCrossingLinesDrawer
The application draws lines between two points selected by the user. The lines are continuous, but not necessarily straight. The lines may not cross any lines previously drawn.

The application was written using .NET Framework 4.7 and WPF technology dedicated to desktop applications. 

In order to check whether it is possible to determine the line between the points indicated by the user, I used the BFS algorithm with predecessors.

The idea behind this problem is to follow the steps:
* preparation of a adjacency matrix, which at the very beginning reflects a pixel table, in which from each pixel you can go to the nearest neighbor from the left, right, top and bottom. For simplicity, you cannot go diagonally,
* having already prepared matrix, it is possible to perform graph searches and write predecessors for each point("pixel"),
* the last step in drawing the first line is to recreate the path ("line") between the two points picked by the user,
* in order to draw another line, algotyhm have to remove all points included in the previous line, so that the new drawn line will not cross the already drawn lines

If there is no way to draw line beetwen selected points, user will get such infromation in MessageBox.

## Example video
<div align="center">
<a href="https://www.youtube.com/watch?v=EDCqrdFl9tA"><img src="https://img.youtube.com/vi/EDCqrdFl9tA/0.jpg" alt="NonCrossingLinesDrawer Example Video"></a>
</div>