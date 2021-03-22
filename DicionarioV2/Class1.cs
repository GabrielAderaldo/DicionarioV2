using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicionarioV2
{

     class NO<TKey, TValue>{

        public TKey key;
        public TValue value;
        public NO<TKey, TValue> next = null;
        public NO(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

	public class Dicionario<K, V> {

		private int MAX;
		private NO<K, V>[] nodes = null;
		private int nThread { get; set; }
		private Object id { get; set; }

		int wait = 1;
		

		public Dicionario()
		{
			init(3);
		}
		public Dicionario(int n,int nthread=1,Object id=null)
		{
			init(n);
			this.nThread = nthread;
			this.id = id;
		}



	private void init(int n)
		{
			MAX = n;
			nodes = new NO<K, V>[MAX];
			for (int i = 0; i < MAX; i++)
				nodes[i] = null;
		}

		private int hash(K key) { return key.GetHashCode() % MAX; }
		public void add(K key, V value)
		{
			//int valor = (int)id;
			//while (wait != valor ) { Console.WriteLine("LOOP"); if (valor == null) { break; } }
			int k = hash(key);
			nodes[k] = addNO(nodes[k], key, value);
			//wait = (wait % nThread) + 1;
		
		}
		private NO<K, V> addNO(NO<K, V> head, K key, V value)
		{
			if (head == null)
			{
				head = new NO<K, V>(key, value);
				return head;
			}
			NO<K, V> h = head;
			while (h.next != null)
			{
				if (Object.ReferenceEquals(h.key,key)) return head;
				h = h.next;
			}
			h.next = new NO<K, V>(key, value);
			return head;
		}

		public void del(K key)
		{
			int k = hash(key);
			nodes[k] = deleteGetHeadNO(nodes[k], key);
		}
		private NO<K, V> deleteGetHeadNO(NO<K, V> head, K key)
		{
			if (head == null) return null;
			if (head.key.Equals(key))
			{
				NO<K, V> n = head.next;
				//free(head);
				return n;
			}
			NO<K, V> del = head;//pode ser head.next
			NO<K, V> ant = head;
			while (del != null)
			{
				if (del.key.Equals(key))
					break;
				ant = del;
				del = del.next;
			}
			if (del != null)
			{
				ant.next = del.next;
				//free(del);
			}
			return head;
		}
		public void toStringHash()
		{
			for (int k = 0; k < MAX; k++)
			{
				Console.WriteLine($"index({k}): ", k);
				if (nodes[k] != null)
					toStringNO(nodes[k]);
				Console.WriteLine($"\n");
			}
		}
		 void toStringNO(NO<K, V> head)
		{
			NO<K, V> end = head;
			while (end != null)
			{
				Console.WriteLine($"{end.key.ToString()},{end.value.ToString()}");
				end = end.next;
			}
		}
		public V valor(K key)
		{
			int k = hash(key);
			return valueNO(nodes[k], key);
		}
		private V valueNO(NO<K, V> head, K key)
		{
			NO<K, V> end = head;
			while (end != null)
			{
				if (end.key.Equals(key)) return end.value;
				end = end.next;
			}
			return default(V);
		}
	}

}
