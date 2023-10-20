using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.U2D;

public static class Global {
    #nullable enable
    public static T FindComponent<T>(GameObject obj) {
        T? returnVal = obj.GetComponent<T>();
        if (returnVal != null && !returnVal.Equals(null)) {
            return returnVal;
        }
        returnVal = obj.GetComponentInChildren<T>();
        if (returnVal != null && !returnVal.Equals(null)) {
            return returnVal;
        }
        returnVal = obj.GetComponentInParent<T>();
        if (returnVal != null && !returnVal.Equals(null)) {
            return returnVal;
        }
        return returnVal;
        
    }
    public static bool SetAlpha(GameObject obj, float alpha) {
        bool spriteAlpha = SetAlpha(FindComponent<SpriteRenderer>(obj), alpha);
        if (spriteAlpha) return spriteAlpha;
        bool shapeAlpha = SetAlpha(FindComponent<SpriteShapeRenderer>(obj), alpha);
        return shapeAlpha;
    }
    public static bool SetAlpha(SpriteRenderer renderer, float alpha) {
        if (renderer == null) {
            return false;
        }
        Color prev = renderer.color;
        prev.a = alpha;
        renderer.color = prev;
        return true;
    }
    public static bool SetAlpha(SpriteShapeRenderer renderer, float alpha) {
        if (renderer == null) {
            return false;
        }
        Color prev = renderer.color;
        prev.a = alpha;
        renderer.color = prev;
        return true;
    }
    public static bool ContainsPoint(Collider coll, Vector2 point) {
        return coll.bounds.Contains(point);
    }
    public static Vector2 GetMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static Vector2 GetRelativeMousePosition(Vector2 relPos) {
        Vector2 pos = relPos - GetMouseWorldPosition();
        pos.Normalize();
        return pos;
    }
}