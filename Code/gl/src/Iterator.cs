using System;

public interface Iterator<T>{
	int getCurrentIndex();
	T next();
	T prev();
	T head();
	T current();
	T tail();
	T getByIndex(int index);
}
