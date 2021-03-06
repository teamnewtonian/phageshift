﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mouseRotate : MonoBehaviour {
    public GameObject projectile;
    public GameObject shootFrom;
    public GameObject reticle;

    public RectTransform coolRect;
    public Text coolText;

    public float cooldown;

    private bool pw=false;
    // Start is called before the first frame update
    void Start() {
        Cursor.visible = true;
        cooldown = 0;

        coolRect = GameObject.Find("HUD/Cooldown/coolVal").GetComponent<RectTransform>();
        coolText = GameObject.Find("HUD/Cooldown/coolText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
        if(Time.timeScale == 1)
        {
            Cursor.visible = false;
        }

        coolRect.sizeDelta = new Vector2((int)10*cooldown, 25);
        coolText.text = "Cooldown: " + (int)cooldown;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);
            reticle.transform.position = new Vector3(target.x, 4.0f, target.z);

            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        if (cooldown < 0)
        {
            cooldown = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown < 10.0f && Time.timeScale == 1) {
            GameObject bullet = Instantiate(projectile, shootFrom.transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
            cooldown += 15* Time.deltaTime;
        }
        else if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (pw) {
            GameObject bullet = Instantiate(projectile, shootFrom.transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("entered" + other.gameObject);
        if (other.tag == "rapidPowerUp") {
            StartCoroutine(PowerUp(3f));
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator PowerUp(float duration) {
        Debug.Log("Power up started");

        pw = true;
        yield return new WaitForSeconds(duration);
        pw = false;

        Debug.Log("Power up ended");
    }

}


