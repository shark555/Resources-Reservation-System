using System;
using System.Collections.Generic;

public abstract class IteratorDefinedHere<T> : Iterator<T>{
	
	protected int curPos = 0;
	protected List<T> lista = new List<T>();
	
	//zwraca aktualny indeks listy
	public int getCurrentIndex(){
		return curPos;
	}
	
	//zwraca kolejny element listy
	public T next(){
		if (curPos + 1 > lista.Count - 1)
			return default(T);
		else{
			curPos++;
			return lista[curPos];
		}
	}
	
	//zwraca poprzedni element listy
	public T prev(){
		if (curPos - 1 < 0)
			return default(T);
		else{
			curPos--;
			return lista[curPos];
		}
	}
	
	//ustawia się na głowie listy i zwraca ten element
	public T head(){
		curPos = 0;
		return lista[0];
	}
	
	//zwraca element, na którym lista jest ustawiona
	public T current(){
		return lista[curPos];
	}
	
	//ustawia się na ogonie listy i zwraca ten element
	public T tail(){
		curPos = lista.Count - 1;
		return lista[lista.Count - 1];
	}
	
	//zwraca null, jeśli index jest poza zakresem 0 - lista.Count
	public T getByIndex(int index){
		if (index > 0 && index < lista.Count)
			return lista[index];
		else
			return default(T);
	}
}
