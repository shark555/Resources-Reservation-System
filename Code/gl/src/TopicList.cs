using System;
using System.Collections.Generic;

public class TopicList : IteratorTopicList{
	
	static private TopicList instance = null;
	private int curPos;
	private List<Topic> lista;
	
	public TopicList(){
		lista = null;
		curPos = 0;
		instance = this;
	}
	
	public int addTopic(Topic addMe){
		lista.Add(addMe);
		curPos++;
		return 0;
	}
	
	//do zrobienia!
	public int changeTopic(int index){
		return 0;
	}
	
	public int deleteTopic(int index){
		if (index < lista.Count && index > 0)
			lista.RemoveAt(index);
		return 0;
	}
	
	public int acceptTopic(int index){
		lista[index].status = 1;
		return 0;
	}
	
	//zwraca -1, jeśli nie znalazł danego tematu w liście
	public int searchTopic(string name){
		int i = 0;
		for (i = 0; i < lista.Count; i++)
			if (lista[i].name == name)
				return i;
		return -1;
	}
	
	public int changeTopicState(int index, int newState){
		if (index > 0 && index < lista.Count)
			lista[index].status = newState;
		return 0;
	}
	
	//zwraca każdy temat w nowej linii
	override public string ToString(){
		string ret = null;
		foreach (Topic t in lista){
			ret += t.ToString();
			ret += "\n";
		}
		return ret;
	}

	static public TopicList getInstance(){	return instance;}
	
	//z interfejsu iteratora
	public int getCurrentIndex(){
		return curPos;
	}
	
	public Topic next(){
		if (curPos + 1 > lista.Count - 1)
			return null;
		else{
			curPos++;
			return lista[curPos];
		}
	}
	
	public Topic prev(){
		if (curPos - 1 < 0)
			return null;
		else{
			curPos--;
			return lista[curPos];
		}
	}
	
	public Topic head(){
		curPos = 0;
		return lista[0];
	}
	
	public Topic current(){
		return lista[curPos];
	}
	
	public Topic tail(){
		curPos = lista.Count - 1;
		return lista[lista.Count - 1];
	}
	
	//zwraca null, jeśli index jest poza zakresem 0 - lista.Count
	public Topic getByIndex(int index){
		if (index > 0 && index < lista.Count)
			return lista[index];
		else
			return null;
	}
}
