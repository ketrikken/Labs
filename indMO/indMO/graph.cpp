#include <functional>
#include <algorithm>
#include <iterator>
#include <iostream>
#include <sstream>
#include <iomanip>
#include <numeric>
#include <fstream>
#include <utility>
#include <cstring>
#include <cstdlib>
#include <cstddef>
#include <vector>
#include <bitset>
#include <cstdio>
#include <cctype>
#include <deque>
#include <stack>
#include <queue>
#include <cmath>
#include <ctime>
#include <list>
#include <map>
#include <set>

using namespace std;

#define mp make_pair
#define pb push_back
#define mref mem_fun_ref
#define bis back_inserter
#define all(x) (x).begin(), (x).end()
#define forn(i, n) for(int64 i = 0; i < int(n); ++i)

typedef long long int64;
typedef pair<int64, int64> pint;
typedef vector<int64> vint;
const int64 INF = 1e9;
const int N = 1e5 + 4;

struct Edge
{
	Edge(){}
	Edge(int _v, int _len) : v(_v), len(_len)  {} 
	int v; // вершина в которую приходим
	int len;
};
struct Node
{
	Node(): dist(INF), prev(-1){}
	Node(int _dist) :dist(_dist) {};
	vector<Edge> edges;
	int dist; // длина пути
	int prev;
};

vector <Node> graph;

void Dij(int s)
{
	priority_queue<pair<int, int>, vector< pair<int, int> >, greater< pair<int, int> > > que; // очередь
	graph[s].dist = 0;
	que.push(mp(0, s));
	while (!que.empty()) 
	{ // пока очередь не пуста
		int u = que.top().second;
		int dist = que.top().first;// достаём расстояние (FIRST!!!)
		que.pop();
		if (graph[u].dist != dist) continue; // если не то, переходим к след.
		for (int i = 0; i < graph[u].edges.size(); ++i) 
		{ // цикл по списку
			int v = graph[u].edges[i].v;
			int newDist = graph[u].dist + graph[u].edges[i].len;
			if (graph[v].dist > newDist) 
			{ // если лучше
				graph[v].dist = newDist; // то улучшаем
				graph[v].prev = u; // запоминаем предшеств.
				que.push(mp(graph[v].dist, v)); // в очередь !!!
			}
		}
	}
}



int main()
{
	freopen("in.txt", "r", stdin);
	//freopen("sum.out", "w", stdout);
	int n;
	cin >> n;
	int firstNode, lastNode;
	cin >> firstNode >> lastNode;
	graph.resize(n);
	int start, end, dist;
	while(cin >> start >> end >> dist)
	{
		start--, end--;
		graph[start].edges.push_back(Edge(end, dist));
		
	}
	
	Dij(--firstNode);
	lastNode--;

	cout << graph[end].dist << endl;
	vint res;
	while (graph[end].prev != -1)
	{
		res.push_back(graph[end].prev + 1);
		end = graph[end].prev;
	}
	
	for(int i = res.size() - 1; i > -1; --i)
	{
		cout << res[i] << ' ';
	}
	cout << endl;
	return 0;
}