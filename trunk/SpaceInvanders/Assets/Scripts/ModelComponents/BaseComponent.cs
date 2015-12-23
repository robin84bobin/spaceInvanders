using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public interface IBaseComponent 
{
	void AddComponent(IBaseComponent child);
	void RemoveComponent(IBaseComponent child);
	void Remove();
	List<IBaseComponent> Children { get; }
	IBaseComponent parent { get; set; }

	void Init();
	void Lock ();
	void Unlock ();
	void Release();
	void Update();
	bool IsSingle { get; }

	void SendMessage(string methodName, params object[] list);
}

public abstract class BaseComponent : IBaseComponent 
{
	private List<IBaseComponent> _children = new List<IBaseComponent> ();
	IBaseComponent _parent;
	public IBaseComponent parent {
		get {
			return _parent;
		}
		set {
			_parent = value;
		}
	}


	protected bool _locked = true;

	public void AddComponent (IBaseComponent child)
	{
		_children.Add (child);
		child.parent = this;
		child.Init ();
	}

	public void RemoveComponent (IBaseComponent child)
	{
		if (_children.Contains (child)) {
			_children.Remove(child);
			child.Release();
			child = null;
		}
	}

	public void Remove ()
	{
		if (parent != null) {
			parent.RemoveComponent (this);
		}

	}

	public List<IBaseComponent> Children {
		get {
			return _children;
		}
	}


	protected bool _isSingle = true;	
	public bool IsSingle{
		get {
			return _isSingle;
		}
	}


	public virtual void Init()
	{
		Unlock ();
		OnInit ();
	}

	public void Lock()
	{
		_locked = true;
	}

	public void Unlock()
	{
		_locked = false;
	}

	public void Release()
	{
		Lock ();
		_children.Clear();
		parent = null;
		OnRelease ();
	}

	protected bool _needToRemove = false;

	public void Update()
	{
		if (_needToRemove) {
			Remove();
			return;
		}

		if (_locked) {
			return;
		}

		OnUpdate ();

		for (int i = 0; i < _children.Count; i++) {
			_children[i].Update();
		}
	}

	Dictionary<string,MethodInfo> _methodsCache = new Dictionary<string, MethodInfo>();
	public void SendMessage (string name, params object[] paramList)
	{
		if (_methodsCache.ContainsKey (name)) {
			_methodsCache[name].Invoke (this, paramList);
			return;
		}

		Type type = this.GetType ();
		MethodInfo method = type.GetMethod (name);
		if (method == null) {
			Debug.LogWarning(String.Format ("Try to Invoke unexisted method {0}::{1}", this.GetType().Name, name));
			return;
		}
		_methodsCache.Add (name, method);
		method.Invoke (this, paramList);
	}

	protected virtual void OnUpdate (){}
	protected virtual void OnInit (){}
	protected virtual void OnRelease (){}

}


