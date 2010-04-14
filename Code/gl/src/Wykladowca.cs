using System;

public class Wykladowca{
	
	string przedmiot;
	
	public Wykladowca(){
		
	}
	
	private int acceptTopic(int index){
		return TopicList.getInstance().acceptTopic(index);
	}
	
	private int deleteTopic(int index){
		return TopicList.getInstance().deleteTopic(index);	
	}
	
	private void acceptChange(int index){
		
	}
	
	private int showTopicsToChange(){
		return 0;
	}
}
