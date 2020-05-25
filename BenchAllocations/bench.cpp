#include <Windows.h>
#include <cstdio>

class Point
{
public:
	int X = 0;
	int Y = 0;
};

static constexpr auto totalAllocations = 1000000;

void programPrealloc()
{
	auto p = new Point();
	for (int i = 0; i < totalAllocations; i++)
	{
		p->X = i;
		p->Y = -i;
	}
	delete p;
}

void programLoopalloc()
{
	for (int i = 0; i < totalAllocations; i++)
	{
		auto p = new Point();
		p->X = i;
		p->Y = -i;
		delete p;
	}
}

int main()
{
	auto total = 0;
	constexpr int count = 100;
	for (int i = 0; i < count; i++)
	{
		auto ticks = GetTickCount();
		programLoopalloc();
		auto ms = GetTickCount() - ticks;
		total += ms;
		printf("program ticks: %ums\n", ms);
	}
	printf("loopalloc total: %ums, average: %ums\n", total, total / count);
	total = 0;
	for (int i = 0; i < count; i++)
	{
		auto ticks = GetTickCount();
		programPrealloc();
		auto ms = GetTickCount() - ticks;
		total += ms;
		printf("program ticks: %ums\n", ms);
	}
	printf("prealloc total: %ums, average: %ums\n", total, total / count);
}