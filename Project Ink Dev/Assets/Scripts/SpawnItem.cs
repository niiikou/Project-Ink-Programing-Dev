using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumSpace;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class SpawnItem : MonoBehaviour{

    Queue<GameObject> queue = new Queue<GameObject>();
    private const string MUSIC_PATH = "music/cut3";
    void SpawnItemFunc()
    {
        if(queue.Count>0)
		{
            var item = queue.Dequeue();
            if (GameMesMananger.Instance().save.itemMap.Find(x => x.Equals(item.name)) == null)
            {
                item.SetActive(true);
                AudioClip clip = Resources.Load<AudioClip>(MUSIC_PATH);
                AudioSource audioSource = item.GetComponent<AudioSource>();
                audioSource.PlayOneShot(clip);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            
            Sequence seq = DOTween.Sequence();
            for(int i=0;i<queue.Count;++i)
            {
                int randNumber = Random.Range(1,10);
                seq.AppendInterval(1.0f+randNumber*0.1f);
                seq.AppendCallback(SpawnItemFunc);
            }
        }
    }

    void OnEnable()
    {
        for(int i=0;i<transform.childCount;++i)
        {
            var go = transform.GetChild(i).gameObject;
            go.AddComponent<AudioSource>();
            queue.Enqueue(go);
            go.SetActive(false);
        }

        GetComponent<BoxCollider>().isTrigger = true;
    }


}