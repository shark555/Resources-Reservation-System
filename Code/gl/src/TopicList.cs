using System;

public class TopicList : IteratorTopicList{
	
	static private TopicList instance = null;
	
	
	public TopicList(){
		instance = this;
	}
	
	public int addTopic(Topic addMe){
		return 0;
	}
	
	public int changeTopic(int index){
		return 0;
	}
	
	public int deleteTopic(int index){
		return 0;
	}
	
	public int acceptTopic(int index){
		return 0;
	}
	
	public int searchTopic(string name){
		return 0;
	}
	
	public int searchTopic(int index){
		return 0;
	}
	
	static public Topic getTopic(int index){
		Topic ret = new Topic();
		return ret;
	}
	
	public int changeTopicState(int index){
		return 0;	
	}
	
	public void showProjectsList(){
			
	}

	static public TopicList getInstance(){	return instance;}
	
	//z interfejsu iteratora
	public int getCurrentIndex(){
		return 0;
	}
	
	public Topic next(){
		return new Topic();
	}
	
	public Topic prev(){
		return new Topic();
	}
	
	public Topic head(){
		return new Topic();
	}
	
	public Topic current(){
		return new Topic();
	}
	
	public Topic tail(){
		return new Topic();
	}
	
	public Topic getByIndex(int index){
		return new Topic();
	}
}
