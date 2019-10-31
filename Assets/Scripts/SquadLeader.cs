using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadLeader : MonoBehaviour
{
    public Transform[] ways;
    public float speed;
    int indexway = 0;
    Quaternion newRot;

    public List<GameObject> members = new List<GameObject>();

    public bool full;
    bool[] pos;

    public GameObject[] positions;


    private void Start()
    {
        Shuffle(ways);
        pos = new bool[positions.Length];
    }


    private void Update()
    {
        if (members.Count > 0)
        {
            foreach (GameObject member in members)
            {
                if (member == null) RemoveMember(member);
            }
        }

        if (members.Count >= positions.Length)
        {
            full = true;
        }
        else full = false;

        Wander();

    }

    void Wander()
    {
        Vector3 dir = ways[indexway].position - transform.position;
        newRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * (speed / 2));
        if (Vector3.Distance(transform.position, ways[indexway].position) < 2)
        {
            indexway++;
            if (indexway == ways.Length) indexway = 0;
        }
    }

    public void RemoveMember(GameObject member)
    {
        pos[members.IndexOf(member)] = false;
        members.Remove(member);
    }

    public void AddMember(GameObject member)
    {
        members.Add(member);
        AssignPosition(member);
    }

    void AssignPosition(GameObject member)
    {
        //pos[members.IndexOf(member)] = true;
        member.GetComponent<AveNovaIA>().slot = positions[members.IndexOf(member)];
    }

    void Shuffle(Transform[] ways)
    {
        System.Random randomNumber = new System.Random();
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < ways.Length; t++)
        {
            Transform tmp = ways[t];
            int r = Random.Range(t, ways.Length);
            ways[t] = ways[r];
            ways[r] = tmp;
        }
    }

}
